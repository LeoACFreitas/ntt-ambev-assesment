using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Sale entity and CreateSale DTOs.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale operation.
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Items, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
            .ForMember(dest => dest.IsCancelled, opt => opt.Ignore());

        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<SaleItem, CreateSaleItemResult>();

        CreateMap<CreateSaleItemRequest, SaleItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DiscountPercentage, opt => opt.Ignore())
            .ForMember(dest => dest.DiscountAmount, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
            .ForMember(dest => dest.IsCancelled, opt => opt.Ignore());
    }
}