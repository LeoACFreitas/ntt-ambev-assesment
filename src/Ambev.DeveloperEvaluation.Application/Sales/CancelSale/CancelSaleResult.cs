namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Response model for CancelSale operation.
/// </summary>
public class CancelSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the cancelled sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the date when the sale was cancelled.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets a message describing the result of the cancellation.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}