using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(item => item.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MinimumLength(2)
            .WithMessage("Product name must be at least 2 characters long.")
            .MaximumLength(100)
            .WithMessage("Product name cannot be longer than 100 characters.");

        RuleFor(item => item.ProductCode)
            .NotEmpty()
            .WithMessage("Product code is required.")
            .MinimumLength(2)
            .WithMessage("Product code must be at least 2 characters long.")
            .MaximumLength(20)
            .WithMessage("Product code cannot be longer than 20 characters.")
            .Matches("^[A-Z0-9-]+$")
            .WithMessage("Product code must contain only uppercase letters, numbers, and hyphens.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20)
            .WithMessage("Cannot sell more than 20 identical items.");

        RuleFor(item => item.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Unit price cannot be negative.")
            .LessThan(999999.99m)
            .WithMessage("Unit price cannot exceed 999,999.99.");

        RuleFor(item => item.DiscountPercentage)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Discount percentage cannot be negative.")
            .LessThanOrEqualTo(1)
            .WithMessage("Discount percentage cannot exceed 100%.");

        RuleFor(item => item.TotalAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total amount cannot be negative.");
    }
}