using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class CreateSaleRequestProfile : Profile
{
    public CreateSaleRequestProfile()
    {
        CreateMap<Sale, SaleDto>();
        CreateMap<SaleItem, SaleItemDto>();

        CreateMap<CreateSaleOutputDto, Sale>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

        CreateMap<CreateSaleItemRequest, SaleItem>()
            .ConvertUsing<BlockSaleItemMapping>();
    }
}

public class BlockSaleItemMapping : ITypeConverter<CreateSaleItemRequest, SaleItem>
{
    public SaleItem Convert(CreateSaleItemRequest source, SaleItem destination, ResolutionContext context)
    {
        throw new InvalidOperationException("Use Sale.AddItem to apply business rules.");
    }
}
