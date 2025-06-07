using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

public class DeleteSaleRequestProfile : Profile
{
    public DeleteSaleRequestProfile()
    {
        CreateMap<DeleteSaleRequest, DeleteSaleCommand>();
        CreateMap<DeleteSaleResult, Application.Sales.DeleteSale.DeleteSaleResult>();
    }
}
