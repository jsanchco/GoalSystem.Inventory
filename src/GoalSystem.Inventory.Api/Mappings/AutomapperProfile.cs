using AutoMapper;
using GoalSystem.Inventory.Api.DTOs;
using GoalSystem.Inventory.Domain.Entities;

namespace GoalSystem.Inventory.Api.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ItemInventory, ItemInventoryDto>();

            CreateMap<ItemInventoryDto, ItemInventory>();
            CreateMap<ItemInventoryDto, ItemInventory>()
                .ConstructUsing(x => new ItemInventory(x.Name, x.ExprirationDate, x.Type));
        }
    }
}
