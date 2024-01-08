﻿using System.ComponentModel.DataAnnotations;

namespace CleanArch.BlazorUI.Models.Identity
{
    public class RegistrationRequestVM
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, EmailAddress]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
