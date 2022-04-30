using GoalSystem.Inventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Application.Services
{
    public interface IItemInventoryService
    {
        Task<List<ItemInventory>> GetAll();
        Task<ItemInventory> GetByName(string filter);
        Task<ItemInventory> Add(ItemInventory itemInventory);
    }
}
