using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity, ISale
{
    public string SaleNumber { get; private set; } = string.Empty;
    public DateTime SaleDate { get; private set; }
    public string CustomerName { get; private set; } = string.Empty;
    public string CustomerEmail { get; private set; } = string.Empty;
    public string CustomerDocument { get; private set; } = string.Empty;
    public string BranchName { get; private set; } = string.Empty;
    public string BranchCode { get; private set; } = string.Empty;
    public List<SaleItem> Items { get; private set; } = [];
    public decimal TotalAmount { get; private set; }
    public bool IsCancelled { get; private set; }

    public Sale()
    {
        SaleDate = DateTime.UtcNow;
    }

    public void AddItem(string productName, string productCode, int quantity, decimal unitPrice)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot add items to a cancelled sale.");

        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name cannot be null or empty.", nameof(productName));

        if (string.IsNullOrWhiteSpace(productCode))
            throw new ArgumentException("Product code cannot be null or empty.", nameof(productCode));

        if (quantity <= 0 || quantity > 20)
            throw new ArgumentException("Quantity must be between 1 and 20.");

        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative.");

        var existingItem = Items.FirstOrDefault(i => i.ProductCode == productCode);
        if (existingItem != null)
        {
            var newQuantity = existingItem.Quantity + quantity;
            if (newQuantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");
            
            existingItem.UpdateQuantity(newQuantity);
        }
        else
        {
            Items.Add(new SaleItem(productName, productCode, quantity, unitPrice));
        }

        RecalculateTotal();
    }

    public void RemoveItem(string productCode)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot remove items from a cancelled sale.");

        var item = Items.FirstOrDefault(i => i.ProductCode == productCode);
        if (item != null)
        {
            Items.Remove(item);
            RecalculateTotal();
        }
    }

    public void UpdateItemQuantity(string productCode, int newQuantity)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot update items in a cancelled sale.");

        if (newQuantity <= 0 || newQuantity > 20)
            throw new ArgumentException("Quantity must be between 1 and 20.");

        var item = Items.FirstOrDefault(i => i.ProductCode == productCode);
        if (item == null)
            throw new InvalidOperationException("Item not found in sale.");

        item.UpdateQuantity(newQuantity);
        RecalculateTotal();
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Sale is already cancelled.");

        IsCancelled = true;
    }

    private void RecalculateTotal()
    {
        TotalAmount = Items.Sum(item => item.TotalAmount);
    }

    /// <summary>
    /// Gets the unique identifier of the sale.
    /// </summary>
    /// <returns>The sale's ID as a string.</returns>
    string ISale.Id => Id.ToString();

    /// <summary>
    /// Gets the sale number.
    /// </summary>
    /// <returns>The sale number.</returns>
    string ISale.SaleNumber => SaleNumber;

    /// <summary>
    /// Gets the total amount of the sale.
    /// </summary>
    /// <returns>The total amount as a string.</returns>
    string ISale.TotalAmount => TotalAmount.ToString("F2");

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}