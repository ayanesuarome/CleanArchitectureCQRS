using Blazored.LocalStorage;
using Blazored.Toast;
using CleanArch.BlazorUI;
using CleanArch.BlazorUI.Handlers;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Providers;
using CleanArch.BlazorUI.Services;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration.Memory;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// In-Memory Configuration
Dictionary<string, string?> data = new()
{
    { "StorageKey", "token" }
};

MemoryConfigurationSource memoryConfig = new()
{
    InitialData = data
};

builder.Configuration.Add(memoryConfig);

// Add Microsoft.Extensions.Http
builder.Services.AddTransient<JwtAuthorizationMessageHandler>();
builder.Services.AddHttpClient<IClient, Client>(
    configureClient: options =>
    {
        options.BaseAddress = new Uri("https://localhost:5001");
        options.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json", 1.0));
    })
    .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();

await builder.Build().RunAsync();
