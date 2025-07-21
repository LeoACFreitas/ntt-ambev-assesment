namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// API response model for CreateSale operation
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// The unique identifier of the created sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The sale number
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// The sale date
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// The customer name
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// The customer email
    /// </summary>
    public string CustomerEmail { get; set; } = string.Empty;

    /// <summary>
    /// The customer document
    /// </summary>
    public string CustomerDocument { get; set; } = string.Empty;

    /// <summary>
    /// The branch name
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// The branch code
    /// </summary>
    public string BranchCode { get; set; } = string.Empty;

    /// <summary>
    /// The total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Whether the sale is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// The list of sale items
    /// </summary>
    public List<CreateSaleItemResponse> Items { get; set; } = new();

    /// <summary>
    /// The date when the sale was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Represents a sale item in the response.
/// </summary>
public class CreateSaleItemResponse
{
    /// <summary>
    /// The unique identifier of the sale item
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product name
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// The product code
    /// </summary>
    public string ProductCode { get; set; } = string.Empty;

    /// <summary>
    /// The quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount percentage applied
    /// </summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// The discount amount
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// The total amount for this item
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Whether the item is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }
}