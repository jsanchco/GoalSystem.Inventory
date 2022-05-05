using GoalSystem.Inventory.Application.Repository;
using GoalSystem.Inventory.Application.Services;
using GoalSystem.Inventory.Domain.Enumerations;
using GoalSystem.Inventory.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Commands
{
    public class RemoveItemInventoryCommandHandler : IRequestHandler<RemoveItemInventoryCommand, bool>
    {
        private readonly ILogger<RemoveItemInventoryCommandHandler> _logger;
        private readonly IRepositoryItemInventory _repositoryItemInventory;
        private readonly IEventsService _eventsService;

        public RemoveItemInventoryCommandHandler(
            ILogger<RemoveItemInventoryCommandHandler> logger,
            IRepositoryItemInventory repositoryItemInventory,
            IEventsService eventsService)
        {
            _logger = logger;
            _repositoryItemInventory = repositoryItemInventory;
            _eventsService = eventsService;
        }

        public async Task<bool> Handle(RemoveItemInventoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _repositoryItemInventory.Delete(request.Name);
            if (!result)
                return false;

            _logger.LogInformation($"Publish Event when the Remove ItemInventory[{request.Name}]");
            return await _eventsService.Publish(new Event(TypeEvent.RemoveItemInventory));
        }
    }
}
