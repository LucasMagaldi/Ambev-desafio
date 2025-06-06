using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// Configura o mapeamento de Sale para GetSalesResponse.
/// </summary>
public class GetSaleResponseProfile : Profile
{
    public GetSaleResponseProfile()
    {
        // Mapeamento da Entity para a camada Application (opcional, se usado)
        CreateMap<Sale, GetSaleResult>();
        CreateMap<SaleItem, GetSaleItemResult>();

        // Mapeamento da camada Application para a camada WebApi (necessário)
        CreateMap<GetSaleResult, GetSaleResponse>();
        CreateMap<GetSaleItemResult, GetSalesItemResponse>();

        // Mapeamento direto da Entity para Response (pode manter se usado em outro ponto)
        CreateMap<Sale, GetSaleResponse>()
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Items.Sum(i => (i.Price * i.Quantity) - i.Discount)));

        CreateMap<SaleItem, GetSalesItemResponse>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => (src.Price * src.Quantity) - src.Discount));
    }
}
