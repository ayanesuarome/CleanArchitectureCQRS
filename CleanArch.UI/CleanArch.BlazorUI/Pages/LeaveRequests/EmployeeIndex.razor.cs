using Blazored.Toast.Services;
using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CleanArch.BlazorUI.Pages.LeaveRequests;

public partial class EmployeeIndex
{
    [Inject] private ILeaveRequestService Service { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IJSRuntime Js { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;

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
        bool confirm = await Js.InvokeAsync<bool>("confirm", "Do you want to cancel this request?");

        if(confirm)
        {
            Response<Guid> response = await Service.CancelLeaveRequestAsync(leaveRequestId);

            if (response.Success)
            {
                ToastService.ShowSuccess("Leave request canceled successfully");
                await OnInitializedAsync();
            }
            else
            {
                Message = response.Message;
            }
        }
    }
}