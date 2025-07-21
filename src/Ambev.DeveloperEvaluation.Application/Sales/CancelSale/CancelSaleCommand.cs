using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command for cancelling a sale.
/// </summary>
public class CancelSaleCommand : IRequest<CancelSaleResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale to cancel.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the CancelSaleCommand.
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    public CancelSaleCommand(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Validates the command using FluentValidation.
    /// </summary>
    /// <returns>Validation result with any errors</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CancelSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}