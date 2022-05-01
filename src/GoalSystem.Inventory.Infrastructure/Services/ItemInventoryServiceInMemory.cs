using GoalSystem.Inventory.Application.Repository;
using GoalSystem.Inventory.Application.Services;
using GoalSystem.Inventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Infrastructure.Services
{
    public class ItemInventoryServiceInMemory : IItemInventoryService
    {
        private readonly IRepositoryItemInventory _repositoryItemInventoryInMemory;

        public ItemInventoryServiceInMemory(IRepositoryItemInventory repositoryItemInventoryInMemory)
        {
            _repositoryItemInventoryInMemory = repositoryItemInventoryInMemory;
        }

        public async Task<List<ItemInventory>> GetAll()
        {
            return await _repositoryItemInventoryInMemory.GetAll();
        }

        public async Task<ItemInventory> GetByName(string filter)
        {
            return await _repositoryItemInventoryInMemory.GetById(filter);
        }

        public async Task<ItemInventory> Add(ItemInventory itemInventory)
        {
            await _repositoryItemInventoryInMemory.Add(itemInventory);

            var result = await GetByName(itemInventory.Name);

            return await Task.FromResult(result);
        }

        public async Task Remove(string code)
        {
            await Task.FromResult(_repositoryItemInventoryInMemory.Delete(code));
        }
    }
}
