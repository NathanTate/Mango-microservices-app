﻿using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {

            return View(await LoadCartDto());
        }

        private async Task<CartDto> LoadCartDto()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)
                .FirstOrDefault()?.Value;
            ResponseDto? response = await _cartService.GetCartByUserIdAsync(userId);
            if(response != null && response.IsSuccess) 
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(response.Result.ToString());
                return cartDto;
            }

            return new CartDto { 
            CartHeader = new CartHeaderDto(),
            CartDetails = new List<CartDetailsDto>()
            };
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            ResponseDto? response = await _cartService.RemoveFromCartAsync(cartDetailsId);
            response.IsSuccess = false;
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            ResponseDto? response = await _cartService.ApplyCouponAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon was applied successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartHeader.CouponCode = "";
            ResponseDto? response = await _cartService.ApplyCouponAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon was applied successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }
    }
}
