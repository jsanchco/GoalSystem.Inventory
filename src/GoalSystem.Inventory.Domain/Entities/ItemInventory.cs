using GoalSystem.Inventory.Domain.Enumerations;
using Newtonsoft.Json;
using System;

namespace GoalSystem.Inventory.Domain.Entities
{
    public class ItemInventory
    {
        public string Name { get; }
        public DateTime ExprirationDate { get; }
        public int Type { get; internal set; }

        public ItemInventory() { }

        [JsonConstructor]
        public ItemInventory(string name, DateTime expirationDate, int type)
        {
            Ensure.Arguments.NotNullOrEmpty(name, nameof(name));
            Ensure.Arguments.InEnum<TypeItemInventory>(name, nameof(type));

            Name = name;
            ExprirationDate = expirationDate;
            Type = type;
        }

        public bool HasExpired => DateTime.UtcNow > ExprirationDate;
    }
}
