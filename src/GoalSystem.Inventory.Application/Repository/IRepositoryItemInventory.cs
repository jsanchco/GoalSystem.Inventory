using GoalSystem.Inventory.Application.Repository.Common;
using GoalSystem.Inventory.Domain.Entities;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Repository
{
    public interface IRepositoryItemInventory : IRepositoryRead<ItemInventory>, IRepositoryAdd<ItemInventory>
    {
        Task<int> Count { get; }
    }
}
