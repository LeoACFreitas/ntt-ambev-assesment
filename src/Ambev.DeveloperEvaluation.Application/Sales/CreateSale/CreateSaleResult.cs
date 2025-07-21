namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Response model for CreateSale operation.
/// </summary>
public class CreateSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the created sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sale date.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the customer name.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer email.
    /// </summary>
    public string CustomerEmail { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer document.
    /// </summary>
    public string CustomerDocument { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch name.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch code.
    /// </summary>
    public string BranchCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the list of sale items.
    /// </summary>
    public List<CreateSaleItemResult> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the date when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Represents a sale item in the result.
/// </summary>
public class CreateSaleItemResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product code.
    /// </summary>
    public string ProductCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage.
    /// </summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// Gets or sets the discount amount.
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Gets or sets the total amount for this item.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets whether the item is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}