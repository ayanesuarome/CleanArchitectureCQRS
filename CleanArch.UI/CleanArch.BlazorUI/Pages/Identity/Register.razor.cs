using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.Identity;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.Identity;

public partial class Register
{
    private string? Message { get; set; }
    private RegistrationRequestVM Model { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; } = null!;

    protected override void OnInitialized()
    {
        Model = new();
    }

    private async Task HandleRegister()
    {
        bool canRegister = await AuthenticationService.RegisterAsync(Model);

        if(!canRegister)
        {
            Message = string.Empty;
        }
    }
}