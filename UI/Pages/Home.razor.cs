using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

namespace CleanArch.BlazorUI.Pages;

public partial class Home
{
    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IAuthenticationService AuthenticationService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProvider) AuthenticationStateProvider).GetAuthenticationStateAsync();
    }

    private void GoToLogin(MouseEventArgs e)
    {
        NavigationManager.NavigateToLogin();
    }

    private async Task Logout(MouseEventArgs e)
    {
        await AuthenticationService.Logout();
    }
    
    private void GoToRegister(MouseEventArgs e)
    {
        NavigationManager.NavigateToRegister();
    }
}