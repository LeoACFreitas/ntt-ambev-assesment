using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Profile for mapping DeleteSale requests and responses
/// </summary>
public class DeleteSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteSale feature
    /// </summary>
    public DeleteSaleProfile()
    {
        CreateMap<Guid, DeleteSaleCommand>()
            .ConstructUsing(id => new DeleteSaleCommand(id));
    }
}