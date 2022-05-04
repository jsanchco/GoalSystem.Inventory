using MediatR;
using System;

namespace GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Commands
{
    public class CreateItemInventoryCommand : IRequest<Domain.Entities.ItemInventory>
    {
        public string Name { get; set; }
        public DateTime ExprirationDate { get; set; }
        public int Type { get; set; }
    }
}
