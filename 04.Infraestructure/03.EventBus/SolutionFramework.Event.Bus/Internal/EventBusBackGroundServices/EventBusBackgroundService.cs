using Microsoft.Extensions.Hosting;
using SolutionFramework.Event.Bus.Abstractions;
using SolutionFramework.Event.Bus.Internal.Queue;

namespace SolutionFramework.Event.Bus.Internal.EventBusBackGroundServices
{
    /// <summary>
    /// Procesa las queue que se registraron al iniciar la aplicación
    /// </summary>
    /// <typeparam name="TEventHandler">Manejador de eventos</typeparam>
    /// <typeparam name="TEvent">Evento de Integración</typeparam>
    public class EventBusBackgroundService<TQueueServices, TEventHandler, TEvent> : BackgroundService, IEventBusBackgroundService<TQueueServices, TEventHandler, TEvent>
        where TQueueServices : IQueueServices<TEventHandler, TEvent>
        where TEventHandler : IEventHandler<TEvent>
        where TEvent : EventBase

    {
        /// <summary>
        /// Servicio que administra los eventos notificados por el event bus
        /// </summary>
        private readonly TQueueServices queueService;

        /// <summary>
        /// Crea una nueva instancia de <see cref="EventBusBackgroundService{TQueueService, TEvent}"/>
        /// </summary>
        /// <param name="queueService">Servicio que administra los eventos notificados por el event bus</param>
        public EventBusBackgroundService(TQueueServices queueService)
        {
            this.queueService = queueService;
        }

        /// <summary>
        /// This method is called when the Microsoft.Extensions.Hosting.IHostedService starts. 
        /// The implementation should return a task that represents the lifetime of the long
        /// running operation(s) being performed.
        /// </summary>
        /// <param name="stoppingToken">Triggered when Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken) is called.</param>
        /// <returns>A System.Threading.Tasks.Task that represents the long running operations.</returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => this.queueService.DequeueAsync(stoppingToken), stoppingToken);
        }
    }
}
