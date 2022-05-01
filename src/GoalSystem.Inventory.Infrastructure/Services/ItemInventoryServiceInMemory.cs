using GoalSystem.Inventory.Application.Repository;
using GoalSystem.Inventory.Application.Services;
using GoalSystem.Inventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Infrastructure.Services
{
    public class ItemInventoryServiceInMemory : IItemInventoryService
    {
        private readonly IRepositoryItemInventory _repositoryItemInventory;
        private readonly ISendEmailService _sendEmailService;

        public ItemInventoryServiceInMemory(
            IRepositoryItemInventory repositoryItemInventoryInMemory,
            ISendEmailService sendEmailService)
        {
            _repositoryItemInventory = repositoryItemInventoryInMemory;
            _sendEmailService = sendEmailService;
        }

        public async Task<List<ItemInventory>> GetAll()
        {
            return await _repositoryItemInventory.GetAll();
        }

        public async Task<ItemInventory> GetByName(string filter)
        {
            return await _repositoryItemInventory.GetById(filter);
        }

        public async Task<ItemInventory> Add(ItemInventory itemInventory)
        {
            await _repositoryItemInventory.Add(itemInventory);

            var result = await GetByName(itemInventory.Name);

            return await Task.FromResult(result);
        }

        public async Task Remove(string code)
        {
            await Task.FromResult(_repositoryItemInventory.Delete(code));


            await Task.FromResult(_sendEmailService.Send());
        }
    }
}
