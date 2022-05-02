using GoalSystem.Inventory.Application.Repository;
using GoalSystem.Inventory.Application.Services;
using GoalSystem.Inventory.Domain.Entities;
using GoalSystem.Inventory.Domain.Enumerations;
using GoalSystem.Inventory.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Infrastructure.Services
{
    /// <summary>
    /// Class that implments the interface IItemInventoryService
    /// </summary>
    public class ItemInventoryServiceInMemory : IItemInventoryService
    {
        private readonly ILogger<ItemInventoryServiceInMemory> _logger;
        private readonly IRepositoryItemInventory _repositoryItemInventory;
        private readonly IEventsService _eventsService;
        
        public ItemInventoryServiceInMemory(
            ILogger<ItemInventoryServiceInMemory> logger,
            IRepositoryItemInventory repositoryItemInventoryInMemory,
            IEventsService eventsService)
        {
            _logger = logger;
            _repositoryItemInventory = repositoryItemInventoryInMemory;
            _eventsService = eventsService;
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

        public async Task<bool> Remove(string code)
        {
            await _repositoryItemInventory.Delete(code);
            _logger.LogInformation($"Publish Event when the Remove ItemInventory[{code}]");

            return await Task.FromResult(await _eventsService.Publish(new Event(TypeEvent.RemoveItemInventory)));
        }
    }
}
