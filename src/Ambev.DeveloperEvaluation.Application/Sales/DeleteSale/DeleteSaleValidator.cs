using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Validator for DeleteSaleCommand that defines validation rules for deleting a sale.
/// </summary>
public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the DeleteSaleCommandValidator with validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must be a valid GUID and not empty
    /// </remarks>
    public DeleteSaleCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Sale ID is required and must be a valid GUID.");
    }
}