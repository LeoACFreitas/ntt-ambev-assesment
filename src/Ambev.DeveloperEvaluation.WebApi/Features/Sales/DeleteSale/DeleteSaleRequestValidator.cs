using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Validator for DeleteSaleRequest
/// </summary>
public class DeleteSaleRequestValidator : AbstractValidator<DeleteSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of DeleteSaleRequestValidator with validation rules.
    /// </summary>
    public DeleteSaleRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Sale ID is required and must be a valid GUID.");
    }
}