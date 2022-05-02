using FluentValidation;
using GoalSystem.Inventory.Api.DTOs;
using System;

namespace GoalSystem.Inventory.Api.Validators
{
    /// <summary>
    /// Class that handler the validation of Item of Inventory with FluenValidation
    /// </summary>
    public class ItemInventoryValidator : AbstractValidator<ItemInventoryDto>
    {
        /// <summary>
        /// Rules to handler one new Item of Inventory
        /// </summary>
        public ItemInventoryValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage($"The name of itemInvnetory must not be empty");

            RuleFor(x => x.Type)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(4)
                .WithMessage($"The type of itemInventory must be greater than or equal to 1 and less than or equal to 4");

            RuleFor(x => x.ExprirationDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage($"The expirationDate of itemInvnetory must be greater than Now");
        }
    }
}
