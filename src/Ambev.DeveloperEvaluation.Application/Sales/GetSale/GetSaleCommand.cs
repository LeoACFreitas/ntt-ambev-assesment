using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Command for retrieving a sale by its ID.
/// </summary>
public class GetSaleCommand : IRequest<GetSaleResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale to retrieve.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the GetSaleCommand.
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    public GetSaleCommand(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Validates the command using FluentValidation.
    /// </summary>
    /// <returns>Validation result with any errors</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new GetSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}