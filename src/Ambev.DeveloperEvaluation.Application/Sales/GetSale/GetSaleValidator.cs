using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Validator for GetSaleCommand that defines validation rules for retrieving a sale.
/// </summary>
public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the GetSaleCommandValidator with validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must be a valid GUID and not empty
    /// </remarks>
    public GetSaleCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Sale ID is required and must be a valid GUID.");
    }
}