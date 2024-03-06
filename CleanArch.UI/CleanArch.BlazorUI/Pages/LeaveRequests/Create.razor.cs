using Blazored.Toast.Services;
using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.LeaveRequests;

public partial class Create
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ILeaveTypeService LeaveTypeService { get; set; } = null!;
    [Inject] private ILeaveRequestService LeaveRequestService { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;

    private LeaveRequestVM LeaveRequestModel { get; set; } = new();
    private List<LeaveTypeVM> LeaveTypesModel { get; set; } = [];
    private string? Message { get; set; }

    protected override async Task OnInitializedAsync()
    {
        LeaveTypesModel = await LeaveTypeService.GetLeaveTypeList();
    }

    private async Task HandleValidSubmitAsync()
    {
        Response<Guid> response = await LeaveRequestService.CreateLeaveRequestAsync(LeaveRequestModel);
        
        if (response.Success)
        {
            ToastService.ShowSuccess("Leave request created successfully");
            NavigationManager.NavigateToEmployeeIndexLeaveRequest();
        }
        else
        {
            Message = response.Message;
        }
    }
}