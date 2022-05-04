using MediatR;
using System.Collections.Generic;

namespace GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Queries
{
    public class GetAllItemsInventoryQuery : IRequest<List<Domain.Entities.ItemInventory>>
    {

    }
}
