namespace Mango.Services.AuthApi.Models.Dto_s
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
