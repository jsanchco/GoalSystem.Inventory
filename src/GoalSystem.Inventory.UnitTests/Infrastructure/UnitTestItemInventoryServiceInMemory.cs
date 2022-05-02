using GoalSystem.Inventory.Domain.Entities;
using GoalSystem.Inventory.Domain.Enumerations;
using GoalSystem.Inventory.Domain.Exceptions;
using GoalSystem.Inventory.Infrastructure.Repository;
using GoalSystem.Inventory.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.UnitTests.Infrastructure
{
    [TestClass]
    public class UnitTestItemInventoryServiceInMemory
    {
        [TestMethod, TestCategory("ItemInventoryServiceInMemory")]
        public async Task Remove_OK()
        {
            var repositoryItemInventoryInMemory = new RepositoryItemInventoryInMemory();
            var code = "Item1";
            var newItemInventory = new ItemInventory(
                code,
                DateTime.UtcNow.AddMinutes(1),
                (int)TypeItemInventory.Type1);
            await repositoryItemInventoryInMemory.Add(newItemInventory);
            Assert.AreEqual(await repositoryItemInventoryInMemory.Count, 1);

            var eventsService = new EventsService(new Mock<ILogger<EventsService>>().Object);
            var itemInventoryServiceInMemory = new ItemInventoryServiceInMemory(
                    new Mock<ILogger<ItemInventoryServiceInMemory>>().Object,
                    repositoryItemInventoryInMemory,
                    eventsService);

            var result = await itemInventoryServiceInMemory.Remove(code);
            Assert.AreEqual(result, true);
            Assert.AreEqual(await repositoryItemInventoryInMemory.Count, 0);
            Console.WriteLine($"Removed Item[{code}] from Repository");
        }

        [TestMethod, TestCategory("ItemInventoryServiceInMemory")]
        public async Task Remove_KO_because_the_item_not_found()
        {
            var repositoryItemInventoryInMemory = new RepositoryItemInventoryInMemory();
            var code = "Item1";
            var newItemInventory = new ItemInventory(
                code,
                DateTime.UtcNow.AddMinutes(1),
                (int)TypeItemInventory.Type1);
            await repositoryItemInventoryInMemory.Add(newItemInventory);
            Assert.AreEqual(await repositoryItemInventoryInMemory.Count, 1);

            var eventsService = new EventsService(new Mock<ILogger<EventsService>>().Object);
            var itemInventoryServiceInMemory = new ItemInventoryServiceInMemory(
                    new Mock<ILogger<ItemInventoryServiceInMemory>>().Object,
                    repositoryItemInventoryInMemory,
                    eventsService);

            var codeFake = "fake";
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await itemInventoryServiceInMemory.Remove(codeFake));
            Assert.AreEqual(ex.Result.Message, $"Not exists one item with the name: {codeFake}", ex.Result.Message);
            Console.WriteLine($"Throw exception controlled: {ex.Result.Message}");
        }
    }
}
