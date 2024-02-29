using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.LeaveRequests;

public partial class EmployeeIndex
{
    [Inject] private ILeaveRequestService Service { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    private EmployeeLeaveRequestVM Model { get; set; } = new();
    private string? Message { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await Service.GetEmployeeLeaveRequestListAsync();
    }

    private void GoToDetails(int id)
    {
        NavigationManager.NavigateToDetailsLeaveRequest(id);
    }

    private async Task CancelRequestAsync(int leaveRequestId)
    {
        Response<Guid> response = await Service.CancelLeaveRequestAsync(leaveRequestId);

        if (response.Success)
        {
            StateHasChanged();
        }
        else
        {
            Message = response.Message;
        }
    }
}