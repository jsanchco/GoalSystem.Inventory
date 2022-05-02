using GoalSystem.Inventory.Domain.Models;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Services
{
    /// <summary>
    /// Interface in charge of handler all Events of the App
    /// </summary>
    public interface IEventsService
    {
        /// <summary>
        /// Publish one event
        /// </summary>
        /// <param name="event">Event to publish</param>
        /// <returns>If the operation is Ok then retrun true, else false</returns>
        Task<bool> Publish(Event @event);
    }
}
