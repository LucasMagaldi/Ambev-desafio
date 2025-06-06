using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class GetSaleByIdProfile : Profile
{
    public GetSaleByIdProfile()
    {
        CreateMap<GetSaleByIdRequest, GetSaleByIdCommand>();
        CreateMap<GetSaleByIdResult, GetSaleByIdResponse>();
        CreateMap<GetSaleItemResult, GetSaleItemResponse>();
    }
}
