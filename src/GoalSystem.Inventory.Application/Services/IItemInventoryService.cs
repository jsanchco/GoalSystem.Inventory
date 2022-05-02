using GoalSystem.Inventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Services
{
    /// <summary>
    /// Interface to handler all related with Item of Inventory
    /// </summary>
    public interface IItemInventoryService
    {
        /// <summary>
        /// Get all Items of Inventory
        /// </summary>
        /// <returns>All items</returns>
        Task<List<ItemInventory>> GetAll();

        /// <summary>
        /// Get Item by name
        /// </summary>
        /// <param name="filter">Filter criteria of search</param>
        /// <returns></returns>
        Task<ItemInventory> GetByName(string filter);

        /// <summary>
        /// Add Item to Inventory
        /// </summary>
        /// <param name="itemInventory">Item of Inventory</param>
        /// <returns>Item found in the Inventory</returns>
        Task<ItemInventory> Add(ItemInventory itemInventory);

        /// <summary>
        /// Remove one Item of Inventory
        /// </summary>
        /// <param name="code">Remove by code</param>
        /// <returns>If found Item and the operation is Ok return true, else false</returns>
        Task<bool> Remove(string code);
    }
}
