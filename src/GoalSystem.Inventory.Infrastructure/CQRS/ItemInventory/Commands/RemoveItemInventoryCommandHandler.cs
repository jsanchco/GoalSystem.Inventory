using GoalSystem.Inventory.Application.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Commands
{
    public class RemoveItemInventoryCommandHandler : IRequestHandler<RemoveItemInventoryCommand, bool>
    {
        private readonly IRepositoryItemInventory _repositoryItemInventory;

        public RemoveItemInventoryCommandHandler(IRepositoryItemInventory repositoryItemInventory)
        {
            _repositoryItemInventory = repositoryItemInventory;
        }

        public async Task<bool> Handle(RemoveItemInventoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _repositoryItemInventory.Delete(request.Name);

            return result;
        }
    }
}
