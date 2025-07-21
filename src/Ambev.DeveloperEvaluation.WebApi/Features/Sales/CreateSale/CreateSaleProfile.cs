using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Profile for mapping CreateSale requests and responses
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale feature
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleItemRequest, Application.Sales.CreateSale.CreateSaleItemRequest>();
        
        CreateMap<CreateSaleResult, CreateSaleResponse>();
        CreateMap<Application.Sales.CreateSale.CreateSaleItemResult, CreateSaleItemResponse>();
    }
}