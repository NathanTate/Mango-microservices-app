using AutoMapper;
using Mango.Services.ShoopingCartAPI.Models;
using Mango.Services.ShoopingCartAPI.Models.Dto_s;

namespace Mango.Services.ProductAPI
{
    public static class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });
        }
    }
}
