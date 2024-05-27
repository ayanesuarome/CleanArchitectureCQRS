using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.Identity;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.Identity;

public partial class Register
{
    private string? Message { get; set; }
    private RegistrationRequestVM Model { get; set; } = new();

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; } = null!;

    private async Task HandleRegister()
    {
        bool canRegister = await AuthenticationService.RegisterAsync(Model);

        if(!canRegister)
        {
            Message = string.Empty;
        }
    }
}