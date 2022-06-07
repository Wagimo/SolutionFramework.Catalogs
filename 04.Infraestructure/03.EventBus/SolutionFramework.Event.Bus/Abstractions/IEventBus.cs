namespace SolutionFramework.Event.Bus.Abstractions
{
    /// <summary>
    /// Esta interface  define el contrato que debe tener la capa de integración que va a interactuar directamente con el servidor de eventBus
    /// como reddis, RabitMq, azure event bus, etc. Todos estos motores tienen caracteristicas similares y es que permiten Emitir y Escuchar eventos
    ///  </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Metodo encargado de publicar un evento de integración
        /// </summary>
        /// <param name="event">Información del evento a publicar</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Tarea que representa la operación  asincrona</returns>
        Task PublishAsync(EventBase @event, CancellationToken cancellationToken);
        /// Metodo encargado de publicar un evento de integración
        /// </summary>
        /// <param name="event">Información del evento a publicar</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Tarea que representa la operación  asincrona</returns>
        /// <returns>Tarea que representa la operación  asincrona, información determinada por la implementación</returns>
        Task PublishAsync<TResult>(EventBase @event, CancellationToken cancellationToken);
        /// <summary>
        /// Metodo encargado de escuchar un evento de integración
        /// </summary>
        /// <typeparam name="TEvent">Evento de Integración a Escuchar</typeparam>
        /// <typeparam name="TEventHandler">Manejador de eventos de integración (CallBack)</typeparam>
        /// <returns>Tarea que representa la operación  asincrona</returns>
        Task SubscribeAsync<TEvent, TEventHandler>()
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>;
        /// <summary>
        /// Metodo encargado de cancelar la suscripción de un evento
        /// </summary>
        /// <typeparam name="TEvent">Evento de integracion a escuchar</typeparam>
        /// <typeparam name="TEventHandler">Manejador de eventos de integración (callback)</typeparam>
        /// <returns>Tarea que representa la operación  asincrona</returns>
        void Unsubscribe<TEvent, TEventHandler>()
           where TEvent : EventBase
           where TEventHandler : IEventHandler<TEvent>;

    }
}
