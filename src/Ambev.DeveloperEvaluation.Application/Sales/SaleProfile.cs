using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales;

namespace Ambev.DeveloperEvaluation.Application.Mappings
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>();
            CreateMap<Sale, CreateSaleResult>();
        }
    }
}
