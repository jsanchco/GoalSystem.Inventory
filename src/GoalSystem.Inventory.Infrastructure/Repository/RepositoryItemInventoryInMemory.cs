using GoalSystem.Inventory.Application.Repository;
using GoalSystem.Inventory.Domain.Ensure;
using GoalSystem.Inventory.Domain.Entities;
using GoalSystem.Inventory.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Infrastructure.Repository
{
    public class RepositoryItemInventoryInMemory : IRepositoryItemInventory
    {
        private List<ItemInventory> _itemsInventory;

        public Task<int> Count => _itemsInventory != null ? Task.FromResult(_itemsInventory.Count()) : Task.FromResult(0);

        public RepositoryItemInventoryInMemory()
        {
            _itemsInventory = new List<ItemInventory>();
        }

        #region Read

        public async Task<List<ItemInventory>> GetAll()
        {
            Arguments.IsNotNullOrEmpty(_itemsInventory, nameof(_itemsInventory));

            return await Task.FromResult(_itemsInventory);
        }

        public async Task<ItemInventory> GetById(string filter)
        {
            Arguments.IsNotNullOrEmpty(_itemsInventory, nameof(_itemsInventory));

            return await Task.FromResult(_itemsInventory.FirstOrDefault(x => x.Name == filter));
        }

        #endregion

        #region Add

        public async Task Add(ItemInventory itemInventory)
        {
            if (_itemsInventory.Any(x => x.Name == itemInventory.Name))
                throw new BusinessException($"Already exists one item with the name: {itemInventory.Name}");

            _itemsInventory.Add(itemInventory);

            await Task.FromResult(0);
        }

        #endregion

        #region Delete

        public async Task Delete(string code)
        {
            var itemToRemove = _itemsInventory.FirstOrDefault(x => x.Name == code);
            if (itemToRemove == null)
                throw new BusinessException($"Not exists one item with the name: {code}");

            _itemsInventory.Remove(itemToRemove);

            await Task.FromResult(0);
        }

        #endregion
    }
}
