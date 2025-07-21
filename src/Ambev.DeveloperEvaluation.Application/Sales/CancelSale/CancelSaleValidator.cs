using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Validator for CancelSaleCommand that defines validation rules for cancelling a sale.
/// </summary>
public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CancelSaleCommandValidator with validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must be a valid GUID and not empty
    /// </remarks>
    public CancelSaleCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Sale ID is required and must be a valid GUID.");
    }
}