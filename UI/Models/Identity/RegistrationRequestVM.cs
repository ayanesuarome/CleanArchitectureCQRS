﻿using System.ComponentModel.DataAnnotations;

namespace CleanArch.BlazorUI.Models.Identity
{
    internal sealed class RegistrationRequestVM
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
