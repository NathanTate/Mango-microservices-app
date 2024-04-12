using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthApi.Models.Dto_s
{
    public class LoginRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
