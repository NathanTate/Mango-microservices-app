using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;   
        }

        public Task<ResponseDto?> CreatetProductAsync(ProductDto product)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Url = ProductAPIBase + $"/api/ProductApi",
                Data = product
            });
        }

        public Task<ResponseDto?> DeleteProductAsync(int id)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.DELETE,
                Url = ProductAPIBase + $"/api/ProductApi/{id}",
            });
        }

        public Task<ResponseDto?> GetAllProductAsync()
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + $"/api/ProductApi",
            }, withBearer: false);
        }

        public Task<ResponseDto?> GetProductAsync(int id)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + $"/api/ProductApi/{id}",
            }, withBearer: false);
        }

        public Task<ResponseDto?> UpdateProductAsync(ProductDto product)
        {
            return _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.PUT,
                Url = ProductAPIBase + $"/api/ProductApi",
                Data = product
            });
        }
    }
}
