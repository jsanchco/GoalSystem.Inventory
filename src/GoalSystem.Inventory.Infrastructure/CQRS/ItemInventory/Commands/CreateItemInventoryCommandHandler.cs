using GoalSystem.Inventory.Application.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Commands
{
    public class CreateItemInventoryCommandHandler : IRequestHandler<CreateItemInventoryCommand, Domain.Entities.ItemInventory>
    {
        private readonly IRepositoryItemInventory _repositoryItemInventory;

        public CreateItemInventoryCommandHandler(IRepositoryItemInventory repositoryItemInventory)
        {
            _repositoryItemInventory = repositoryItemInventory;
        }

        public async Task<Domain.Entities.ItemInventory> Handle(CreateItemInventoryCommand request, CancellationToken cancellationToken)
        {
            await _repositoryItemInventory.Add(new Domain.Entities.ItemInventory(request.Name, request.ExprirationDate, request.Type));

            var result = await _repositoryItemInventory.GetById(request.Name);

            return result;
        }
    }
}
