using Microsoft.Extensions.DependencyInjection;
using SolutionFramework.Core.Abstractions;
using SolutionFramework.Event.Bus.Abstractions;
using SolutionFramework.Event.Bus.Exceptions;
using SolutionFramework.Event.Bus.Internal.EventBusBackGroundServices;
using SolutionFramework.Event.Bus.Internal.Queue;
using System.Reflection;

namespace SolutionFramework.Event.Bus.Extension
{
    /// <summary>
    /// Metodo de extension que registra eventHandler al ServiceCollection (ioC)
    /// </summary>
    public static class EventBusExtension
    {
        /// <summary>
        /// Adiciona al ServiceCollection los tipos <see cref="IEventbus "/> y <see cref="ISubscriptionManager"/>
        /// </summary>
        /// <param name="services">IServiceCollection utilizado para adicionar los servicios</param>
        /// <returns>Una referencia a esta instancia despues de que haya terminado la operación</returns>
        /// <exception cref="EventNotImplementedException"></exception>
        public static IServiceCollection AddeventBus(this IServiceCollection services)
        {
            services.AddSingleton<ISubscriptionManager, SubscriptionManager>();

            var eventBus = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(assembly => assembly.GetTypes())
                            .FirstOrDefault(x =>
                                typeof(IEventBus).IsAssignableFrom(x)
                                && x.IsClass
                                && !x.IsInterface
                                && !x.IsAbstract);
            if (eventBus == null)
                throw new EventNotImplementedException();
            services.AddSingleton(typeof(IEventBus), eventBus);
            return services;
        }


        public static IServiceCollection AddEventsHandlers<TStartupLogic>(this IServiceCollection services)
            where TStartupLogic : IStartupServices
        {
            var eventHandlers = GetEventsHandlers<TStartupLogic>();

            foreach (var eventHandler in eventHandlers)
            {
                var interfaceEventHandlerGeneric = eventHandler.GetInterfaces().FirstOrDefault(x => x.IsGenericType
                                                       && x.GetGenericTypeDefinition() == typeof(IEventHandler<>));

                // A partir de la interface generica, obtenemos los argumentos Genericos que hereden de la subclase EventBase
                var eventType = interfaceEventHandlerGeneric?
                                    .GetGenericArguments()
                                    .FirstOrDefault(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(EventBase)));

                //Creando la queue generica y host service generico
                if (eventType != null && interfaceEventHandlerGeneric != null)
                {
                    //creando la Queue Generica, primero la Interface Firma => IQueueServices<TEventHandler, in TEvent> e inyectando parametros
                    var queueServiceType = typeof(IQueueServices<,>).MakeGenericType(eventHandler, eventType);

                    //Creando la implementación Firma=> QueueService<TEventHandler, TEvent> e inyectnadole los parametros 
                    var queueServiceImplementationType = typeof(QueueService<,>).MakeGenericType(eventHandler, eventType);

                    //Creando el Host Service Firma => IEventBusBackgroundService<TQueueServices, TEventHandler, TEvent> e inyectando parametros
                    var hostServiceType = typeof(IEventBusBackgroundService<,,>).MakeGenericType(queueServiceImplementationType, eventHandler, eventType);

                    //Creando la implementación de HostServices  Firma => EventBusBackgroundService<TQueueServices,TEventHandler, TEvent> e inyectando los tipos
                    var hostServiceImplementationType = typeof(EventBusBackgroundService<,,>).MakeGenericType(queueServiceImplementationType, eventHandler, eventType);

                    services.AddSingleton(queueServiceType, queueServiceImplementationType);
                    services.AddTransient(hostServiceType, hostServiceImplementationType);
                    services.AddTransient(eventHandler);
                }


            }
            return services;
        }

        /// <summary>
        /// Registramos los eventHandlers en el SubscriptionManager que ese encuentren en el ensablando  donde esta la clase TStartupLogic que implementala
        /// interface IStartupServices. En este metodo no se registran servicios sino que se hacen Uso de los servicios registrados y construidos. Este metodo no se usa en el cConfigurationServices del
        /// StartUp sino en el Metodo Configure
        /// </summary>
        /// <typeparam name="TStartuoLogic"></typeparam>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IServiceProvider SubscribeEventsHandlers<TStartupLogic>(this IServiceProvider provider)
            where TStartupLogic : IStartupServices
        {
            var subscriptionManager = provider.GetRequiredService<ISubscriptionManager>();
            var typeSubscriptionManager = subscriptionManager.GetType();
            var methodAddSubscription = typeSubscriptionManager.GetMethods().FirstOrDefault(x => x.Name.Contains("AddSubscription"));

            var eventBus = provider.GetRequiredService<IEventBus>();
            var eventBusType = eventBus.GetType();

            var eventHandlers = GetEventsHandlers<TStartupLogic>();


            foreach (var eventHandler in eventHandlers)
            {
                //Obtiene la Interface Generica que implementa la clase ( IEventHandler<>
                var interfaceEventHandlerGeneric = eventHandler.GetInterfaces().FirstOrDefault(x => x.IsGenericType
                                                      && x.GetGenericTypeDefinition() == typeof(IEventHandler<>));
                if (interfaceEventHandlerGeneric != null)
                {
                    //Obtiene el Tipo registrado en la Interface Generica  IEventHandler< -> UserCreateEvent <- >
                    var eventType = interfaceEventHandlerGeneric
                                  .GetGenericArguments()
                                  .FirstOrDefault(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(EventBase)));
                    if (eventType != null)
                    {
                        if (!eventType.IsGenericParameter)
                        {
                            var methodAdd = methodAddSubscription.MakeGenericMethod(eventType, eventHandler);
                            methodAdd.Invoke(subscriptionManager, null);

                            // Firma -> SubscribeAsync<TEvent, TEventHandler>()
                            //Buscando el metodo generico dentro dela clase
                            var methodSubscribe = eventBusType.GetMethods()
                                .FirstOrDefault(x => x.Name == nameof(IEventBus.SubscribeAsync) && x.IsGenericMethod);
                            if (methodSubscribe != null)
                            {
                                var methosGeneric = methodSubscribe.MakeGenericMethod(eventType, eventHandler); //Inyectando los parámetros
                                (methosGeneric.Invoke(eventBus, null) as Task).ConfigureAwait(false); //Invocando el metodo
                            }

                        }
                    }
                }
            }



            return provider;
        }

        /// <summary>
        /// Escanea las clases que implementan la interface <see cref="IEventHandler<>"/> y por cada EventHandler encontrado , debe crearse una
        /// Queue Generica y un Host service generico.
        /// </summary>
        /// <typeparam name="TStartupLogic"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="EventNotImplementedException"></exception>

        public static List<Type> GetEventsHandlers<TStartupLogic>()
            where TStartupLogic : IStartupServices
        {
            //identificamos si en un ensamblado, un conjunto de clases implementan una interface generica
            return Assembly.GetAssembly(typeof(TStartupLogic)) //En Un ensamblado
                            .GetTypes() //conjunto de clases
                            .Where(x => x.IsClass
                                && x.IsAssignableGenericFrom(typeof(IEventHandler<>))
                                ).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="interface"></param>
        /// <returns></returns>
        public static bool IsAssignableGenericFrom(this Type type, Type @interface)
        {
            //se busca la interface generica @interface dentro del conjunto de interfaces
            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == @interface);
        }

    }
}
