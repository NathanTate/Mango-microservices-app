﻿using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthApi.Models.Dto_s
{
    public class UserDto
    {
        public string ID { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength (25)]
        public string PhoneNumber { get; set; }
    }
}
