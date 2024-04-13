using Mango.Services.ShoopingCartAPI.Models.Dto_s;

namespace Mango.Services.ShoopingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
