using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Configura o mapeamento de Sale para GetSalesResponse.
/// </summary>
public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Sale, GetSaleResponse>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Items.Sum(i => (i.Price * i.Quantity) - i.Discount)));

        CreateMap<SaleItem, GetSalesItemResponse>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => (src.Price * src.Quantity) - src.Discount));
    }
}
