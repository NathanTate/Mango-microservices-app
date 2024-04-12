using Mango.Services.AuthApi.Models.Dto_s;

namespace Mango.Services.AuthApi.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDto registerDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
