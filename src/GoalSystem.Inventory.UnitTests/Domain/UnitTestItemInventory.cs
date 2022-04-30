using GoalSystem.Inventory.Domain.Entities;
using GoalSystem.Inventory.Domain.Enumerations;
using GoalSystem.Inventory.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace Codere.MX.FeedCorner.UnitTests.Domain
{
    [TestClass]
    public class UnitTestRepositoryFeedsFromJson
    {
        [TestMethod, TestCategory("ItemInventory")]
        public void Create_Feed_OK()
        {
            var name = "ItemInventory1";
            var type = TypeItemInventory.Type1;
            var expirationDate = DateTime.UtcNow;

            var itemInventory = new ItemInventory(name, expirationDate, (int)type);
            Assert.AreEqual(itemInventory.Name, name);
            Assert.AreEqual(itemInventory.Type, type);
            Assert.AreEqual(itemInventory.ExprirationDate, expirationDate);
            Console.WriteLine($"Created ItemInventory: {JsonConvert.SerializeObject(itemInventory, Formatting.Indented)}");
        }

        [TestMethod, TestCategory("ItemInventory")]
        public void Create_Feed_KO_code_NotNullOrEmpty()
        {
            var name = "";
            var type = TypeItemInventory.Type1;
            var expirationDate = DateTime.UtcNow;

            var ex = Assert.ThrowsException<BusinessException>(() => new ItemInventory(name, expirationDate, (int)type));
            Assert.AreEqual(ex.Message, "The argument 'name' must not be null or empty", ex.Message);
            Console.WriteLine($"Throw exception controlled: {ex.Message}");
        }

        [TestMethod, TestCategory("ItemInventory")]
        //[ExpectedException(typeof(BusinessException), "The argument 'stadium' must be in 'Sultanes, Mariachis, Diablos or Rayados'")]
        public void Create_Feed_KO_stadium_InEnum()
        {
            var name = "ItemInventory1";
            var type = 5;
            var expirationDate = DateTime.UtcNow;

            var ex = Assert.ThrowsException<BusinessException>(() => new ItemInventory(name, expirationDate, type));
            Assert.AreEqual(ex.Message, $"The value '{type}' is not in 'TypeItemInventory'", ex.Message);
            Console.WriteLine($"Throw exception controlled: {ex.Message}");
        }

        [TestMethod, TestCategory("ItemInventory")]
        public void ItemInventory_with_expirationDate_out()
        {
            var name = "ItemInventory1";
            var type = TypeItemInventory.Type1;
            var expirationDate = DateTime.UtcNow.AddMinutes(-1);

            var itemInventory = new ItemInventory(name, expirationDate, (int)type);
            Assert.AreEqual(itemInventory.Name, name);
            Assert.AreEqual(itemInventory.ExprirationDate, expirationDate);
            Assert.AreEqual(itemInventory.Type, (int)type);

            Assert.AreEqual(itemInventory.HasExpired, true);
            Console.WriteLine($"The ItemInventory [{JsonConvert.SerializeObject(itemInventory, Formatting.Indented)} has expired]");
        }
    }
}
