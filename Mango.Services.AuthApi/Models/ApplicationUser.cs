using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
