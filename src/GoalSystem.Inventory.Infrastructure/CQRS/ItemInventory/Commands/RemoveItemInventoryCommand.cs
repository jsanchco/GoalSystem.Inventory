using MediatR;

namespace GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Commands
{
    public class RemoveItemInventoryCommand : IRequest<bool>
    {
        public string Name { get; set; }
    }
}
