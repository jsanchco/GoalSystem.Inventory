using MediatR;

namespace GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Queries
{
    public class GetByNameItemInventoryQuery : IRequest<Domain.Entities.ItemInventory>
    {
        public string Name { get; set; }
    }
}
