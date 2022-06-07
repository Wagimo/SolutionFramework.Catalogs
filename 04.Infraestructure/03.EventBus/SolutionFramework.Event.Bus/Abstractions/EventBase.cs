using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace SolutionFramework.Event.Bus.Abstractions
{
    /// <summary>
    /// Cuando se emite un evento se crea a partir del eventBus
    /// </summary>
    public abstract class EventBase : IEquatable<EventBase>
    {

        /// <summary>
        /// Inicializa una nueva instancia de la class SolutionFramework.Event.Bus.Abstractions.EventBse
        /// </summary>
        protected EventBase()
        {
            IdEvent = Guid.NewGuid();
            EventDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase SolutionFramework.Event.Bus.Abstractions.EventBse con informacion especifica de Id y Date event
        /// </summary>
        /// <param name="idEvent">Id Evento</param>
        /// <param name="eventDate">Fecha Evento</param>
        /// <exception cref="ArgumentOutOfRangeException">Asignacion de fecha invalida</exception>
        [JsonConstructor]
        protected EventBase(Guid idEvent, DateTime eventDate)
        {
            if (eventDate == DateTime.MinValue)
                throw new ArgumentOutOfRangeException(nameof(eventDate));

            IdEvent = idEvent;
            EventDate = eventDate;
        }

        /// <summary>
        /// Establece u Obtiene el Id del Evento
        /// </summary>
        [JsonProperty]
        public Guid IdEvent { get; private set; }

        /// <summary>
        /// Establece u Obtiene la fecha del evento
        /// </summary>
        [JsonProperty]
        public DateTime EventDate { get; private set; }

        /// <summary>
        /// Indica si el objeto actual es igual a otro objeto del mismo tipo
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Retorna Verdadero si el objeto actual es igual al objeto pasado en el parámetro other, en caso contrario es falso.</returns>
        /// Valida si el objeto ya fue registrado en el sistema de encolamiento
        public bool Equals([AllowNull] EventBase? other)
        {
            return IdEvent == other.IdEvent;
        }
    }
}
