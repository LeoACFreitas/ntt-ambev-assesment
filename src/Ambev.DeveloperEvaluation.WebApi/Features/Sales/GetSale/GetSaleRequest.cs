namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Represents a request to retrieve a sale by its ID.
/// </summary>
public class GetSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale to retrieve.
    /// </summary>
    public Guid Id { get; set; }
}