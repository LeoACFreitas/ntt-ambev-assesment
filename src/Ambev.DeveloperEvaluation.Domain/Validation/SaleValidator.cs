using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number is required.");

        RuleFor(sale => sale.CustomerName)
            .NotEmpty()
            .WithMessage("Customer name is required.");

        RuleFor(sale => sale.CustomerEmail)
            .NotEmpty()
            .WithMessage("Customer email is required.")
            .EmailAddress()
            .WithMessage("Customer email must be a valid email address.");

        RuleFor(sale => sale.CustomerDocument)
            .MaximumLength(20)
            .WithMessage("Customer document cannot be longer than 20 characters.");

        RuleFor(sale => sale.BranchName)
            .NotEmpty()
            .WithMessage("Branch name is required.");

        RuleFor(sale => sale.BranchCode)
            .MaximumLength(10)
            .WithMessage("Branch code cannot be longer than 10 characters.");

        RuleFor(sale => sale.SaleDate)
            .NotEqual(default(DateTime))
            .WithMessage("Sale date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1))
            .WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.TotalAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total amount cannot be negative.");

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("Sale must have at least one item.")
            .Must(items => items.Count <= 100)
            .WithMessage("Sale cannot have more than 100 items.");
    }
}