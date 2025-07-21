namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Defines the contract for representing a sale item in the system.
    /// </summary>
    public interface ISaleItem
    {
        /// <summary>
        /// Gets the unique identifier of the sale item.
        /// </summary>
        /// <returns>The item ID as a string.</returns>
        public string Id { get; }

        /// <summary>
        /// Gets the product quantity.
        /// </summary>
        /// <returns>The quantity as a string.</returns>
        public string Quantity { get; }

        /// <summary>
        /// Gets the total amount of the item.
        /// </summary>
        /// <returns>The total amount of the item as a string.</returns>
        public string TotalAmount { get; }
    }
}