namespace SolutionFramework.Event.Bus.Abstractions
{
    /// <summary>
    /// Interface base para implementar un manejador de eventos a partir de un evento definido
    /// cuando se escucha un evento se debe crear una clase que implemente esta Interface (IEventHandler)
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<in TEvent> where TEvent : EventBase
    {
        /// <summary>
        /// Invocado por el Event Bus cuando se detecta un evento al que se está suscrito. (Escucha los eventos emitidos por el EventBus)
        /// </summary>
        /// <param name="data">Información del Evento</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Tarea que representa la operación asincrona</returns>
        Task HandleAsync(TEvent data, CancellationToken cancellationToken);
    }
}
