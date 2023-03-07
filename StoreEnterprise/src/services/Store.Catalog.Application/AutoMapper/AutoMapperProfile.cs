using AutoMapper;
using Store.Catalog.Domain.Entities;
using Store.Catalog.Shared.Core.RequestModels;
using Store.Catalog.Shared.Core.ResponseModels;

namespace Store.Catalog.Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProductRequest, Product>();
        CreateMap<Product, ProductResponse>();
    }
}
