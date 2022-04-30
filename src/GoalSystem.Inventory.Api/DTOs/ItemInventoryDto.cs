using Newtonsoft.Json;
using System;

namespace GoalSystem.Inventory.Api.DTOs
{
    public class ItemInventoryDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("exprirationDate")]
        public DateTime ExprirationDate { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
    }
}
