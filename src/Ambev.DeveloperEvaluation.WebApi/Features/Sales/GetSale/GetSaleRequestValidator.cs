using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Validator for GetSaleRequest
/// </summary>
public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of GetSaleRequestValidator with validation rules.
    /// </summary>
    public GetSaleRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Sale ID is required and must be a valid GUID.");
    }
}