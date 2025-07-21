using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Profile for mapping CancelSale requests and responses
/// </summary>
public class CancelSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CancelSale feature
    /// </summary>
    public CancelSaleProfile()
    {
        CreateMap<Guid, CancelSaleCommand>()
            .ConstructUsing(id => new CancelSaleCommand(id));
            
        CreateMap<CancelSaleResult, CancelSaleResponse>();
    }
}