using GoalSystem.Inventory.Application.Repository;
using GoalSystem.Inventory.Application.Services;
using GoalSystem.Inventory.Infrastructure.Repository;
using GoalSystem.Inventory.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GoalSystem.Inventory.Api.Configuration
{
    /// <summary>
    /// Class to handler all generics services of the Application 
    /// </summary>
    public static class ServicesCollectionExtension
    {
        /// <summary>
        /// Add all services commons
        /// </summary>
        /// <param name="services">collection of all services</param>
        /// <returns>collection of all services included the commons</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IRepositoryItemInventory, RepositoryItemInventoryInMemory>();
            services.AddTransient<IItemInventoryService, ItemInventoryServiceInMemory>();
            services.AddSingleton<IEventsService, EventsService>();

            return services;
        }
    }
}
