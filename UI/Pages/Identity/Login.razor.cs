using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.Identity;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.Identity;

public partial class Login
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;

    private LoginVM Model { get; set; } = new();
    public string? Message { get; set; }

    private async Task HandleLogin()
    {
        bool canAuthenticate = await AuthenticationService.AuthenticateAsync(Model);

        if(!canAuthenticate)
        {
            Message = "Username/password combination unknown";
        }
        else
        {
            NavigationManager.NavigateToHome();
        }
    }
}