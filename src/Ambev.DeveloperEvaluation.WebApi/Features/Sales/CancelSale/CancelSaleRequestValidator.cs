using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Validator for CancelSaleRequest
/// </summary>
public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of CancelSaleRequestValidator with validation rules.
    /// </summary>
    public CancelSaleRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Sale ID is required and must be a valid GUID.");
    }
}