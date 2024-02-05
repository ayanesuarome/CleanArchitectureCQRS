using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.LeaveRequests;

public partial class Index
{
    [Inject] private ILeaveRequestService Service { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    private AdminLeaveRequestVM Model { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Response<AdminLeaveRequestVM> response = await Service.GetAdminLeaveRequestListAsync();

        if (response.Success)
        {
            Model = response.Data;
        }
    }

    private void GoToDetails(int id)
    {
        NavigationManager.NavigateToDetailsLeaveRequest(id);
    }
}