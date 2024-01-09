using CleanArch.BlazorUI.Models.Identity;

namespace CleanArch.BlazorUI.Interfaces;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(LoginVM model);
    Task Logout();
    Task<bool> RegisterAsync(RegistrationRequestVM model);
}
