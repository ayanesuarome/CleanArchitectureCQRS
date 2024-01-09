using System.ComponentModel.DataAnnotations;

namespace CleanArch.BlazorUI.Models.Identity;

public class LoginVM
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
