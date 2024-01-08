namespace CleanArch.BlazorUI.Interfaces;

public interface IAuthenticationService
{
    Task<bool> Authenticate(string email, string password);
    Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password);
    Task Logout();
}
