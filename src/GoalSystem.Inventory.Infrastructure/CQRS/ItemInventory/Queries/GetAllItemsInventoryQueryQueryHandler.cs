using GoalSystem.Inventory.Application.Repository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Queries
{
    public class GetAllItemsInventoryQueryQueryHandler : IRequestHandler<GetAllItemsInventoryQuery, List<Domain.Entities.ItemInventory>>
    {
        private readonly IRepositoryItemInventory _repositoryItemInventory;

        public GetAllItemsInventoryQueryQueryHandler(IRepositoryItemInventory repositoryItemInventory)
        {
            _repositoryItemInventory = repositoryItemInventory;
        }

        public async Task<List<Domain.Entities.ItemInventory>> Handle(GetAllItemsInventoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _repositoryItemInventory.GetAll();

            return result;
        }
    }
}
