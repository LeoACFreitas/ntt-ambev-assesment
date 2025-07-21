using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations.
/// Defines the contract for sale data access following the repository pattern.
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Creates a new sale in the repository.
    /// </summary>
    /// <param name="sale">The sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale</returns>
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a sale by its sale number.
    /// </summary>
    /// <param name="saleNumber">The sale number</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all sales with optional filtering and pagination.
    /// </summary>
    /// <param name="skip">Number of sales to skip</param>
    /// <param name="take">Number of sales to take</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of sales</returns>
    Task<IEnumerable<Sale>> GetAllAsync(int skip = 0, int take = 100, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves sales by customer name.
    /// </summary>
    /// <param name="customerName">The customer name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of sales for the customer</returns>
    Task<IEnumerable<Sale>> GetByCustomerNameAsync(string customerName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves sales by branch code.
    /// </summary>
    /// <param name="branchCode">The branch code</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of sales for the branch</returns>
    Task<IEnumerable<Sale>> GetByBranchCodeAsync(string branchCode, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves sales within a date range.
    /// </summary>
    /// <param name="startDate">The start date</param>
    /// <param name="endDate">The end date</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of sales within the date range</returns>
    Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing sale.
    /// </summary>
    /// <param name="sale">The sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale</returns>
    Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a sale by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sale was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the total count of sales.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Total count of sales</returns>
    Task<int> GetCountAsync(CancellationToken cancellationToken = default);
}