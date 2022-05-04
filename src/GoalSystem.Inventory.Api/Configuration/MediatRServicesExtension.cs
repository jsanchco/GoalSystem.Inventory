using GoalSystem.Inventory.Domain.Entities;
using GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Commands;
using GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace GoalSystem.Inventory.Api.Configuration
{
    public static class MediatRServicesExtension
    {
        public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IRequestHandler<GetAllItemsInventoryQuery, List<ItemInventory>>, GetAllItemsInventoryQueryQueryHandler>();
            services.AddTransient<IRequestHandler<GetByNameItemInventoryQuery, ItemInventory>, GetByNameItemInventoryQueryHandler>();
            services.AddTransient<IRequestHandler<CreateItemInventoryCommand, ItemInventory>, CreateItemInventoryCommandHandler>();
            services.AddTransient<IRequestHandler<RemoveItemInventoryCommand, bool>, RemoveItemInventoryCommandHandler>();

            return services;
        }
    }
}
