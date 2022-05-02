using GoalSystem.Inventory.Domain.Enumerations;
using System;

namespace GoalSystem.Inventory.Domain.Models
{
    /// <summary>
    /// Class in charge all related with the events that will be published
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Date of published
        /// </summary>
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;

        /// <summary>
        /// Enum of Type Event
        /// </summary>
        public readonly TypeEvent _typeEvent;


        /// <summary>
        /// Constructor that handle the inmutability of object 
        /// </summary>
        /// <param name="typeEvent"></param>
        public Event(TypeEvent typeEvent)
        {
            _typeEvent = typeEvent;

            DateOccurred = DateTimeOffset.UtcNow;
        }


        /// <summary>
        /// Override ToString in order to more information about the event
        /// </summary>
        /// <returns>More information</returns>
        public override string ToString()
        {
            return $"Event Occurred [{DateOccurred:dd/MM/yyyy HH:mm:ss}], Type [{Enum.GetName(typeof(TypeEvent), _typeEvent)}]";
        }
    }
}
