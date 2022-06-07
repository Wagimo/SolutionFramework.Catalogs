using SolutionFramework.Event.Bus.Abstractions;
using SolutionFramework.Event.Bus.Internal.Queue;

namespace SolutionFramework.Event.Bus.Internal.EventBusBackGroundServices
{
    /// <summary>
    /// Procesa las queue que se registraron al iniciar la aplicación
    /// </summary>
    /// <typeparam name="TEventHandler">Manejador de eventos</typeparam>
    /// <typeparam name="TEvent">Evento de Integración</typeparam>
    public interface IEventBusBackgroundService<TQueueServices, TEventHandler, TEvent>
        where TQueueServices : IQueueServices<TEventHandler, TEvent>
        where TEventHandler : IEventHandler<TEvent>
        where TEvent : EventBase

    {
    }
}
