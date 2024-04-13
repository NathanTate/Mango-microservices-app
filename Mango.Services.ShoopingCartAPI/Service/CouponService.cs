using Mango.Services.ShoopingCartAPI.Models.Dto_s;
using Mango.Services.ShoopingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Mango.Services.ShoopingCartAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            
            var apiResponse = await client.GetAsync($"/api/CouponApi/GetByCode/{couponCode}");
            var response = await apiResponse.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(response);
            if(resp != null && resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(resp.Result.ToString());
            }

            return new CouponDto();
        }
    }
}
