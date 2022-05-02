using GoalSystem.Inventory.Application.Repository.Common;
using GoalSystem.Inventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Repository
{
    /// <summary>
    /// Interface to objects handlers of Items of Inventory
    /// </summary>
    public interface IRepositoryItemInventory : IRepositoryRead<ItemInventory>, IRepositoryAdd<ItemInventory>, IRepositoryDelete<ItemInventory>
    {
        /// <summary>
        /// Count of Item of Inventory
        /// </summary>
        Task<int> Count { get; }

        /// <summary>
        /// Get all Items of Inventory that have expired
        /// </summary>
        /// <returns></returns>
        Task<List<ItemInventory>> GetExpired();
    }
}
