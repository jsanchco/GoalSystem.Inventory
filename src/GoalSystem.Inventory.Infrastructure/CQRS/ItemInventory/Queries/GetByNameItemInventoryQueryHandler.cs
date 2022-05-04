using GoalSystem.Inventory.Application.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Queries
{
    public class GetByNameItemInventoryQueryHandler : IRequestHandler<GetByNameItemInventoryQuery, Domain.Entities.ItemInventory>
    {
        private readonly IRepositoryItemInventory _repositoryItemInventory;

        public GetByNameItemInventoryQueryHandler(IRepositoryItemInventory repositoryItemInventory)
        {
            _repositoryItemInventory = repositoryItemInventory;
        }

        public async Task<Domain.Entities.ItemInventory> Handle(GetByNameItemInventoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _repositoryItemInventory.GetById(request.Name);

            return result;
        }
    }
}
