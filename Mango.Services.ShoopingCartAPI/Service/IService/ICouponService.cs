using Mango.Services.ShoopingCartAPI.Models.Dto_s;

namespace Mango.Services.ShoopingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
