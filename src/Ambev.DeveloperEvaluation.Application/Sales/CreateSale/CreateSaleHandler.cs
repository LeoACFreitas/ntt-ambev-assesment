using AutoMapper;
using MediatR;
using FluentValidation;
using FluentValidation.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler.
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request.
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Check if sale number already exists
        var existingSale = await _saleRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
        if (existingSale != null)
            throw new InvalidOperationException($"Sale with number {command.SaleNumber} already exists");

        // Create the sale entity
        var sale = new Sale();
        
        // Set basic sale properties using reflection or direct assignment since properties are private set
        var saleType = typeof(Sale);
        saleType.GetProperty("SaleNumber")?.SetValue(sale, command.SaleNumber);
        saleType.GetProperty("SaleDate")?.SetValue(sale, command.SaleDate);
        saleType.GetProperty("CustomerName")?.SetValue(sale, command.CustomerName);
        saleType.GetProperty("CustomerEmail")?.SetValue(sale, command.CustomerEmail);
        saleType.GetProperty("CustomerDocument")?.SetValue(sale, command.CustomerDocument);
        saleType.GetProperty("BranchName")?.SetValue(sale, command.BranchName);
        saleType.GetProperty("BranchCode")?.SetValue(sale, command.BranchCode);

        // Add items to the sale
        foreach (var itemRequest in command.Items)
        {
            sale.AddItem(itemRequest.ProductName, itemRequest.ProductCode, itemRequest.Quantity, itemRequest.UnitPrice);
        }

        // Validate the created sale
        var saleValidation = sale.Validate();
        if (!saleValidation.IsValid)
        {
            var errors = saleValidation.Errors.Select(e => new ValidationFailure(e.Error, e.Detail));
            throw new ValidationException(errors);
        }

        // Save the sale
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        
        // Map to result
        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }
}