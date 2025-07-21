namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Response model for DeleteSale operation.
/// </summary>
public class DeleteSaleResponse
{
    /// <summary>
    /// Gets or sets whether the deletion was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets a message describing the result of the deletion.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}