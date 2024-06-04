using System.ComponentModel.DataAnnotations;

namespace CleanArch.BlazorUI.Models.Identity;

internal sealed class LoginVM
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
