using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.LeaveRequests;

public partial class Create
{
    private LeaveRequestVM LeaveRequestModel { get; set; } = new();
    private List<LeaveTypeVM> LeaveTypesModel { get; set; } = [];

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ILeaveTypeService LeaveTypeService { get; set; } = null!;
    [Inject] private ILeaveRequestService LeaveRequestService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        LeaveTypesModel = await LeaveTypeService.GetLeaveTypeList();
    }

    private async Task HandleValidSubmitAsync()
    {
        Response<Guid> response = await LeaveRequestService.CreateLeaveRequestAsync(LeaveRequestModel);
        NavigationManager.NavigateToIndexLeaveRequest();
    }
}