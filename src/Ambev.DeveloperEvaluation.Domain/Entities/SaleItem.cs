using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity, ISaleItem
{
    public string ProductName { get; private set; } = string.Empty;
    public string ProductCode { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }

    protected SaleItem() { }

    public SaleItem(string productName, string productCode, int quantity, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name cannot be null or empty.", nameof(productName));
        
        if (string.IsNullOrWhiteSpace(productCode))
            throw new ArgumentException("Product code cannot be null or empty.", nameof(productCode));
        
        if (quantity <= 0 || quantity > 20)
            throw new ArgumentException("Quantity must be between 1 and 20.");
        
        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative.");

        ProductName = productName.Trim();
        ProductCode = productCode.Trim().ToUpperInvariant();
        Quantity = quantity;
        UnitPrice = unitPrice;
        IsCancelled = false;
        
        CalculateAmounts();
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot update quantity of a cancelled item.");

        if (newQuantity <= 0 || newQuantity > 20)
            throw new ArgumentException("Quantity must be between 1 and 20.");

        Quantity = newQuantity;
        CalculateAmounts();
    }

    public void UpdateUnitPrice(decimal newUnitPrice)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot update price of a cancelled item.");

        if (newUnitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative.");

        UnitPrice = newUnitPrice;
        CalculateAmounts();
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Item is already cancelled.");

        IsCancelled = true;
    }

    private void CalculateAmounts()
    {
        var subtotal = Quantity * UnitPrice;

        switch (Quantity)
        {
            case >= 10 and <= 20:
                DiscountPercentage = 0.20m;
                break;
            case >= 4 and < 10:
                DiscountPercentage = 0.10m;
                break;
            default:
                DiscountPercentage = 0.00m;
                break;
        }

        DiscountAmount = subtotal * DiscountPercentage;
        TotalAmount = subtotal - DiscountAmount;
    }

    /// <summary>
    /// Gets the unique identifier of the sale item.
    /// </summary>
    /// <returns>The sale item's ID as a string.</returns>
    string ISaleItem.Id => Id.ToString();

    /// <summary>
    /// Gets the quantity.
    /// </summary>
    /// <returns>The quantity as a string.</returns>
    string ISaleItem.Quantity => Quantity.ToString();

    /// <summary>
    /// Gets the total amount of the sale item.
    /// </summary>
    /// <returns>The total amount as a string.</returns>
    string ISaleItem.TotalAmount => TotalAmount.ToString("F2");

    /// <summary>
    /// Performs validation of the sale item entity using the SaleItemValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}