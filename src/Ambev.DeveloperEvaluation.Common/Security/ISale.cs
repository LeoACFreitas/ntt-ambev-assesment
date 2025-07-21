namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Defines the contract for representing a sale in the system.
    /// </summary>
    public interface ISale
    {
        /// <summary>
        /// Gets the unique identifier of the sale.
        /// </summary>
        /// <returns>The sale ID as a string.</returns>
        public string Id { get; }

        /// <summary>
        /// Gets the sale number.
        /// </summary>
        /// <returns>The sale number.</returns>
        public string SaleNumber { get; }

        /// <summary>
        /// Gets the total amount of the sale.
        /// </summary>
        /// <returns>The total amount of the sale as a string.</returns>
        public string TotalAmount { get; }
    }
}