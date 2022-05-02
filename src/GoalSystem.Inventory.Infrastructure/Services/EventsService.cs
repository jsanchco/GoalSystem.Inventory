using GoalSystem.Inventory.Application.Services;
using GoalSystem.Inventory.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Infrastructure.Services
{
    /// <summary>
    /// Class that implements the interface IEventsService
    /// </summary>
    public class EventsService : IEventsService
    {
        private readonly ILogger<EventsService> _logger;

        public EventsService(ILogger<EventsService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> Publish(Event @event)
        {
            _logger.LogInformation($"Publish event: {@event}");

            return await Task.FromResult(true);
        }
    }
}
