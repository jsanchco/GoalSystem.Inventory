using FluentValidation;
using GoalSystem.Inventory.Api.DTOs;
using System;

namespace GoalSystem.Inventory.Api.Validators
{
    public class ItemInventoryValidator : AbstractValidator<ItemInventoryDto>
    {
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
