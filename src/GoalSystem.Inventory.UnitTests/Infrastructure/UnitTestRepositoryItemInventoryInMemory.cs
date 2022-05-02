using GoalSystem.Inventory.Domain.Entities;
using GoalSystem.Inventory.Domain.Enumerations;
using GoalSystem.Inventory.Domain.Exceptions;
using GoalSystem.Inventory.Infrastructure.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.UnitTests.Infrastructure
{
    [TestClass]
    public class UnitTestRepositoryItemInventoryInMemory
    {
        [TestMethod, TestCategory("RepositoryItemInventoryInMemory")]
        public async Task Add_OK()
        {
            var repositoryItemInventoryInMemory = new RepositoryItemInventoryInMemory();
            var newItemInventory = new ItemInventory(
                "Item1",
                DateTime.UtcNow.AddMinutes(1),
                (int)TypeItemInventory.Type1);
            await repositoryItemInventoryInMemory.Add(newItemInventory);

            Assert.AreEqual(await repositoryItemInventoryInMemory.Count, 1);
            Console.WriteLine($"Add one item to Repository ItemInventory [{repositoryItemInventoryInMemory.Count}] items");
        }

        [TestMethod, TestCategory("RepositoryItemInventoryInMemory")]
        public async Task Add_KO_because_exist_one_item_with_thes_same_name()
        {
            var repositoryItemInventoryInMemory = new RepositoryItemInventoryInMemory();
            var newItemInventory = new ItemInventory(
                "Item1",
                DateTime.UtcNow.AddMinutes(1),
                (int)TypeItemInventory.Type1);
            await repositoryItemInventoryInMemory.Add(newItemInventory);

            Assert.AreEqual(await repositoryItemInventoryInMemory.Count, 1);
            Console.WriteLine($"Add one item to Repository ItemInventory [{repositoryItemInventoryInMemory.Count}] items");

            var itemInventoryWithEqualName = new ItemInventory(
                "Item1",
                DateTime.UtcNow.AddMinutes(2),
                (int)TypeItemInventory.Type2);
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryItemInventoryInMemory.Add(itemInventoryWithEqualName));
            Assert.AreEqual(ex.Result.Message, $"Already exists one item with the name: {itemInventoryWithEqualName.Name}", ex.Result.Message);
            Console.WriteLine($"Throw exception controlled: {ex.Result.Message}");
        }

        [TestMethod, TestCategory("RepositoryItemInventoryInMemory")]
        public async Task Get_OK()
        {
            var repositoryItemInventoryInMemory = new RepositoryItemInventoryInMemory();
            var newItemInventory = new ItemInventory(
                "Item1",
                DateTime.UtcNow.AddMinutes(1),
                (int)TypeItemInventory.Type1);
            await repositoryItemInventoryInMemory.Add(newItemInventory);

            Assert.AreEqual(await repositoryItemInventoryInMemory.Count, 1);
            Console.WriteLine($"Add one item to Repository ItemInventory [{repositoryItemInventoryInMemory.Count}] items");

            var result = await repositoryItemInventoryInMemory.GetAll();
            Assert.AreEqual(await repositoryItemInventoryInMemory.Count, result.Count);
            Assert.AreEqual(result[0].Name, newItemInventory.Name);
            Console.WriteLine($"The first element of ItemInventory Repositroy is [{JsonConvert.SerializeObject(newItemInventory, Formatting.Indented)}]");
        }

        [TestMethod, TestCategory("RepositoryItemInventoryInMemory")]
        public async Task GetById_OK()
        {
            var repositoryItemInventoryInMemory = new RepositoryItemInventoryInMemory();
            var code = "Item1";
            var newItemInventory = new ItemInventory(
                code,
                DateTime.UtcNow.AddMinutes(1),
                (int)TypeItemInventory.Type1);
            await repositoryItemInventoryInMemory.Add(newItemInventory);

            Assert.AreEqual(await repositoryItemInventoryInMemory.Count, 1);
            Console.WriteLine($"Add one item to Repository ItemInventory [{repositoryItemInventoryInMemory.Count}] items");

            var result = await repositoryItemInventoryInMemory.GetById(code);
            Assert.AreEqual(result.Name, code);
            Console.WriteLine($"The element[{code}] of ItemInventory Repositroy is [{JsonConvert.SerializeObject(newItemInventory, Formatting.Indented)}]");
        }

        [TestMethod, TestCategory("RepositoryItemInventoryInMemory")]
        public async Task Remove_OK()
        {
            var repositoryItemInventoryInMemory = new RepositoryItemInventoryInMemory();
            var code = "Item1";
            var newItemInventory = new ItemInventory(
                code,
                DateTime.UtcNow.AddMinutes(1),
                (int)TypeItemInventory.Type1);
            await repositoryItemInventoryInMemory.Add(newItemInventory);
            var newItemInventory1 = new ItemInventory(
                "Item2",
                DateTime.UtcNow.AddMinutes(1),
                (int)TypeItemInventory.Type1);
            await repositoryItemInventoryInMemory.Add(newItemInventory1);

            Assert.AreEqual(await repositoryItemInventoryInMemory.Count, 2);
            Console.WriteLine($"Add two items to Repository ItemInventory [{repositoryItemInventoryInMemory.Count}] items");

            await repositoryItemInventoryInMemory.Delete(code);
            var itemInventory = await repositoryItemInventoryInMemory.GetById(code);
            Assert.IsNull(itemInventory);

            Console.WriteLine($"Remove element[{code}] of ItemInventory Repositroy");
        }

        [TestMethod, TestCategory("RepositoryItemInventoryInMemory")]
        public async Task Remove_KO_when_the_code_of_item_not_exists()
        {
            var repositoryItemInventoryInMemory = new RepositoryItemInventoryInMemory();
            var newItemInventory = new ItemInventory(
                "Item1",
                DateTime.UtcNow.AddMinutes(1),
                (int)TypeItemInventory.Type1);
            await repositoryItemInventoryInMemory.Add(newItemInventory);
            var newItemInventory1 = new ItemInventory(
                "Item2",
                DateTime.UtcNow.AddMinutes(1),
                (int)TypeItemInventory.Type1);

            var code = "fake";
            await repositoryItemInventoryInMemory.Add(newItemInventory1);
            var ex = Assert.ThrowsExceptionAsync<BusinessException>(async () => await repositoryItemInventoryInMemory.Delete(code));
            Assert.AreEqual(ex.Result.Message, $"Not exists one item with the name: {code}", ex.Result.Message);
            Console.WriteLine($"Throw exception controlled: {ex.Result.Message}");
        }
    }
}
