namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// API response model for CancelSale operation
/// </summary>
public class CancelSaleResponse
{
    /// <summary>
    /// The unique identifier of the cancelled sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The sale number
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Whether the sale is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// The date when the sale was cancelled
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// A message describing the result of the cancellation
    /// </summary>
    public string Message { get; set; } = string.Empty;
}