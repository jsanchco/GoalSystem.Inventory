using AutoMapper;
using GoalSystem.Inventory.Api.DTOs;
using GoalSystem.Inventory.Domain.Entities;

namespace GoalSystem.Inventory.Api.Mappings
{
    /// <summary>
    /// Class handler all mapping in DTO's and reverse with Automapper
    /// </summary>
    public class AutomapperProfile : Profile
    {
        /// <summary>
        /// Add of possibles mappings
        /// </summary>
        public AutomapperProfile()
        {
            CreateMap<ItemInventory, ItemInventoryDto>();

            CreateMap<ItemInventoryDto, ItemInventory>();
            CreateMap<ItemInventoryDto, ItemInventory>()
                .ConstructUsing(x => new ItemInventory(x.Name, x.ExprirationDate, x.Type));
        }
    }
}
