using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto_s;

namespace Mango.Services.ProductAPI
{
    public static class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<ProductDto, Product>();
            });
        }
    }
}
