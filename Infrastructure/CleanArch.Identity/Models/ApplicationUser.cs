using Microsoft.AspNetCore.Identity;

namespace CleanArch.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
