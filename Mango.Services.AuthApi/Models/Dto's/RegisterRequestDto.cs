using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthApi.Models.Dto_s
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(25)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [MaxLength(100)]
        public string? Role { get; set; }
    }
}
