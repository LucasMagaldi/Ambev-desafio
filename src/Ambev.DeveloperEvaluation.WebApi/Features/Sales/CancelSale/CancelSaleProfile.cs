using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

public class CancelSaleResponseProfile : Profile
{
    public CancelSaleResponseProfile()
    {
        CreateMap<CancelSaleResponse, CancelSaleResponse>();
    }
}
