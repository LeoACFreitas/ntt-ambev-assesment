using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of CreateSaleRequestValidator with validation rules.
    /// </summary>
    public CreateSaleRequestValidator()
    {
        RuleFor(request => request.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number is required.")
            .Length(1, 50)
            .WithMessage("Sale number must be between 1 and 50 characters.");

        RuleFor(request => request.CustomerName)
            .NotEmpty()
            .WithMessage("Customer name is required.")
            .Length(2, 100)
            .WithMessage("Customer name must be between 2 and 100 characters.");

        RuleFor(request => request.CustomerEmail)
            .NotEmpty()
            .WithMessage("Customer email is required.")
            .EmailAddress()
            .WithMessage("Customer email must be a valid email address.");

        RuleFor(request => request.CustomerDocument)
            .MaximumLength(20)
            .WithMessage("Customer document cannot be longer than 20 characters.");

        RuleFor(request => request.BranchName)
            .NotEmpty()
            .WithMessage("Branch name is required.")
            .Length(2, 100)
            .WithMessage("Branch name must be between 2 and 100 characters.");

        RuleFor(request => request.BranchCode)
            .NotEmpty()
            .WithMessage("Branch code is required.")
            .Length(2, 20)
            .WithMessage("Branch code must be between 2 and 20 characters.");

        RuleFor(request => request.SaleDate)
            .NotEqual(default(DateTime))
            .WithMessage("Sale date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1))
            .WithMessage("Sale date cannot be in the future.");

        RuleFor(request => request.Items)
            .NotEmpty()
            .WithMessage("Sale must have at least one item.")
            .Must(items => items.Count <= 100)
            .WithMessage("Sale cannot have more than 100 items.");

        RuleForEach(request => request.Items)
            .SetValidator(new CreateSaleItemRequestValidator());
    }
}

/// <summary>
/// Validator for CreateSaleItemRequest
/// </summary>
public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of CreateSaleItemRequestValidator with validation rules.
    /// </summary>
    public CreateSaleItemRequestValidator()
    {
        RuleFor(item => item.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .Length(2, 100)
            .WithMessage("Product name must be between 2 and 100 characters.");

        RuleFor(item => item.ProductCode)
            .NotEmpty()
            .WithMessage("Product code is required.")
            .Length(2, 20)
            .WithMessage("Product code must be between 2 and 20 characters.")
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
    }
}