using SolutionFramework.Event.Bus.Abstractions;

namespace SolutionFramework.Event.Bus.Internal.Queue
{
    /// <summary>
    /// Servicio que administra los eventos notificados por el event bus
    /// </summary>
    /// <typeparam name="TEventHandler">Evento de Integracion</typeparam>
    /// <typeparam name="TEvent">manejador de eventos</typeparam>
    public interface IQueueServices<TEventHandler, in TEvent>
        where TEventHandler : IEventHandler<TEvent>
        where TEvent : EventBase
    {
        /// <summary>
        /// Agrega un objeto al final de la cola
        /// </summary>
        /// <param name="event">Objeto a agregar al final de la Queue</param>
        /// <exception cref="ArgumentNullException">Se genera cuando <paramref name="event"/> es nulo</exception>
        void Enqueue(TEvent @event);

        /// <summary>
        /// intenta eliminar y devolver el objeto al comienzo de la cola concurrente
        /// </summary>
        /// <param name="cancellationToken">Toten de cancelacion</param>
        /// <returns>Tarea que representa la operacion asincrona</returns>
        Task DequeueAsync(CancellationToken cancellationToken);
    }
}
