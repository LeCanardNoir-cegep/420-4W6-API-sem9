﻿using System.ComponentModel.DataAnnotations;

namespace UserToken.Models
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
