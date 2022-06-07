using CodeDesignPlus.Redis.Event.Bus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SolutionFramework.Event.Bus;
using SolutionFramework.Event.Bus.Abstractions;
using SolutionFramework.Event.Bus.Internal.Queue;
using StackExchange.Redis;

namespace SolutionFramework.Redis.Event.Bus
{
    /// <summary>
    /// Default implementation of the <see cref="IRedisEventBusService"/>
    /// </summary>
    public class RedisEventBusService : IRedisEventBusService
    {
        /// <summary>
        /// Service logger
        /// </summary>
        private readonly ILogger<RedisEventBusService> logger;
        /// <summary>
        /// Service that management connection with Redis Server
        /// </summary>
        private readonly IRedisService redisService;
        /// <summary>
        /// Service that management the events and events handlers inside assembly
        /// </summary>
        private readonly ISubscriptionManager subscriptionManager;
        /// <summary>
        /// Service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initialize a new instance of the <see cref="RedisEventBusService"/>
        /// </summary>
        /// <param name="redisService">Service that management connection with Redis Server</param>
        /// <param name="subscriptionManager">Service that management the events and events handlers inside assembly</param>
        /// <param name="serviceProvider">Service provider</param>
        /// <param name="logger">Service logger</param>
        public RedisEventBusService(IRedisService redisService, ISubscriptionManager subscriptionManager, IServiceProvider serviceProvider, ILogger<RedisEventBusService> logger)
        {
            this.redisService = redisService ?? throw new ArgumentNullException(nameof(redisService));
            this.subscriptionManager = subscriptionManager ?? throw new ArgumentNullException(nameof(subscriptionManager));
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Posts a message to the given channel.
        /// </summary>
        /// <param name="event">The event to publish.</param>
        /// <param name="token">The cancellation token that will be assigned to the new task.</param>
        /// <returns>Return a <see cref="Task"/></returns>
        /// <exception cref="ArgumentNullException">@event is null</exception>
        public Task PublishAsync(EventBase @event, CancellationToken token)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            var channell = @event.GetType().Name;

            var message = JsonConvert.SerializeObject(@event);

            return redisService.Subscriber.PublishAsync(channell, message);

            //return this.PrivatePublishAsync<long>(@event);
        }

        /// <summary>
        /// Posts a message to the given channel.
        /// </summary>
        /// <typeparam name="TResult">Type result (long)</typeparam>
        /// <param name="event">The event to publish.</param>
        /// <param name="token">The cancellation token that will be assigned to the new task.</param>
        /// <returns>The number of clients that received the message.</returns>
        /// <exception cref="ArgumentNullException">@event is null</exception>
        public Task PublishAsync<TResult>(EventBase @event, CancellationToken token)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            return this.PrivatePublishAsync<TResult>(@event);
        }



        /// <summary>
        /// Posts a message to the given channel.
        /// </summary>
        /// <typeparam name="TResult">Type result (long)</typeparam>
        /// <param name="event">The event to publish.</param>
        /// <returns>The number of clients that received the message.</returns>
        private async Task<TResult> PrivatePublishAsync<TResult>(EventBase @event)
        {
            var channel = @event.GetType().Name;

            var message = JsonConvert.SerializeObject(@event);

            var notified = await this.redisService.Subscriber.PublishAsync(channel, message);

            this.logger.LogDebug($"The number of clients notified {notified} in the channel {channel} with the next message {message}");

            return (TResult)Convert.ChangeType(notified, typeof(TResult));
        }

        /// <summary>
        /// This method is invoked when register the subscribe with <see cref="CodeDesignPlus.Event.Bus.Extensions.EventBusExtensions.SubscribeEventsHandlers{TStartupLogic}(IServiceProvider)"/> extension method
        /// Subscribe to perform some operation when a message to the preferred/active node is broadcast, without any guarantee of ordered handling.
        /// </summary>
        /// <typeparam name="TEvent">Type Event</typeparam>
        /// <typeparam name="TEventHandler">Type Event Handler</typeparam>
        /// <returns>Return a <see cref="Task"/></returns>
        public async Task SubscribeAsync<TEvent, TEventHandler>()
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>
        {
            var channel = typeof(TEvent).Name;

            logger.LogDebug($"Register client in the channel {channel}");

            await redisService.Subscriber.SubscribeAsync(channel, (_, v) => ListenerEvent<TEvent, TEventHandler>(v));
        }

        /// <summary>
        /// The handler to invoke when a message is received on channel.
        /// </summary>
        /// <typeparam name="TEvent">Type Event</typeparam>
        /// <typeparam name="TEventHandler">Type Event Handler</typeparam>
        /// <param name="value">The value received</param>
        public void ListenerEvent<TEvent, TEventHandler>(RedisValue value)
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>
        {
            this.logger.LogDebug($"Message received on the channel {typeof(TEvent).Name} with message {value}");

            //Debemos obtener la queue encargada de procesar el evento. Esto se hace a partir del subcriptionmanager
            //el cual registra los eventos y a partir de ahi, se obtiene la queue.
            if (this.subscriptionManager.HasSubscriptionsForEvent<TEvent>())
            {
                //Obtenemos las suscripciones asociadas al evento
                var subscriptions = this.subscriptionManager.FindSubscriptions<TEvent>();

                //Cada suscripcion nos retorna el evento y el eventHandler. y acada tupla tiene una queue para poder encolar 
                //el evento.
                foreach (var subscription in subscriptions)
                {
                    this.logger.LogDebug($"The message will add to the queue with event {subscription.EventType.Name} and the handler {subscription.EventHandlerType.Name}");
                    //se crea un queuetype
                    var queueType = typeof(IQueueServices<,>);

                    //se le inyectan los parametros genericos para obtener la definicion
                    queueType = queueType.MakeGenericType(subscription.EventHandlerType, subscription.EventType);

                    //con la defincion le pedimos al sistema de inyección de dependencias  que nos devuelva la queue
                    var queue = this.serviceProvider.GetService(queueType);

                    //
                    var @event = JsonConvert.DeserializeObject<TEvent>(value);

                    //invocamos el metodo Enqueue a partir de reflexion
                    queue.GetType().GetMethod(nameof(IQueueServices<TEventHandler, TEvent>.Enqueue))
                        .Invoke(queue, new object[] { @event });

                    this.logger.LogDebug($"The message was added successfully");
                }
            }
        }

        /// <summary>
        /// Unsubscribe from a specified message channel
        /// </summary>
        /// <typeparam name="TEvent">Type Event</typeparam>
        /// <typeparam name="TEventHandler">Type Event Handler</typeparam>
        public void Unsubscribe<TEvent, TEventHandler>()
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>
        {
            var channel = typeof(TEvent).Name;

            this.logger.LogDebug($"Remove subscription of the channel {channel}");

            this.subscriptionManager.RemoveSubscription<TEvent, TEventHandler>();

            this.redisService.Subscriber.Unsubscribe(channel);
        }


    }
}
