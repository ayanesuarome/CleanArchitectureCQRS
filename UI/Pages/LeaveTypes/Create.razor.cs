using Blazored.Toast.Services;
using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.LeaveTypes;

public partial class Create
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ILeaveTypeService LeaveTypeService { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;
    
    private string? Message { get; set; }
    private LeaveTypeVM Model { get; set; } = new();

    private async Task CreateLeaveTypeAsync()
    {
        Response<Guid> response = await LeaveTypeService.CreateLeaveType(Model);

        if (response.Success)
        {
            ToastService.ShowSuccess("Leave type created successfully");
            NavigationManager.NavigateToIndexLeaveType();
        }
        else
        {
            Message = response.Message;
        }
    }
}