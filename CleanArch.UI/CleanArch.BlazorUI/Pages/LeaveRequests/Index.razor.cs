using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.LeaveRequests;

public partial class Index
{
    [Inject] private ILeaveRequestService Service { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    private AdminLeaveRequestVM Model { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Model = await Service.GetAdminLeaveRequestListAsync();
    }

    private void GoToDetails(int id)
    {
        NavigationManager.NavigateToDetailsLeaveRequest(id);
    }
}