using GoalSystem.Inventory.Application.Repository;
using GoalSystem.Inventory.Application.Services;
using GoalSystem.Inventory.Infrastructure.Repository;
using GoalSystem.Inventory.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GoalSystem.Inventory.Api.Configuration
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IRepositoryItemInventory, RepositoryItemInventoryInMemory>();
            services.AddTransient<IItemInventoryService, ItemInventoryServiceInMemory>();

            return services;
        }
    }
}
