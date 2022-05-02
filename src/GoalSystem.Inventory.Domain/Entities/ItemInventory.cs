using GoalSystem.Inventory.Domain.Enumerations;
using Newtonsoft.Json;
using System;

namespace GoalSystem.Inventory.Domain.Entities
{
    /// <summary>
    /// Class of Item of Inventory
    /// </summary>
    public class ItemInventory
    {
        /// <summary>
        /// Name of Item
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Date of expiration of Item
        /// </summary>
        public DateTime ExprirationDate { get; }

        /// <summary>
        /// Type of Item (must be between the values of Enum 'TypeItemInventory')
        /// </summary>
        public int Type { get; internal set; }

        /// <summary>
        /// Contructor empty of class
        /// </summary>
        public ItemInventory() { }

        /// <summary>
        /// Constructor of ItemInvetory that support the inmutability
        /// </summary>
        /// <param name="name">Name of Item</param>
        /// <param name="expirationDate">Date of expiration of the Item</param>
        /// <param name="type">Type of the Item (must be between the values of Enum 'TypeItemInventory')</param>
        [JsonConstructor]
        public ItemInventory(string name, DateTime expirationDate, int type)
        {
            Ensure.Arguments.NotNullOrEmpty(name, nameof(name));
            Ensure.Arguments.InEnum<TypeItemInventory>(type, nameof(TypeItemInventory));

            Name = name;
            ExprirationDate = expirationDate;
            Type = type;
        }

        /// <summary>
        /// Evalue if one Item has expired compared with the actual UTC Date
        /// </summary>
        public bool HasExpired => DateTime.UtcNow > ExprirationDate;
    }
}
