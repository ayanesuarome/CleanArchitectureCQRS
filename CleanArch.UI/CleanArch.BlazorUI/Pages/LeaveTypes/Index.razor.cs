using Blazored.Toast.Services;
using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace CleanArch.BlazorUI.Pages.LeaveTypes;

public partial class Index
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ILeaveTypeService LeaveTypeService { get; set; } = null!;
    [Inject] private ILeaveAllocationService LeaveAllocationService { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private IJSRuntime Js { get; set; } = null!;

    private IReadOnlyCollection<LeaveTypeVM>? LeaveTypes { get; set; }
    public string? Message { get; set; }

    protected override async Task OnInitializedAsync()
    {
        LeaveTypes = await LeaveTypeService.GetLeaveTypeList();
    }

    private void CreateLeaveType(MouseEventArgs e)
    {
        NavigationManager.NavigateToCreateLeaveType();
    }

    private async Task AllocateLeaveType(int id)
    {
        await LeaveAllocationService.CreateLeaveAllocations(id);
    }

    private void EditLeaveType(int id)
    {
        NavigationManager.NavigateToEditLeaveType(id);
    }
    
    private void DetailsLeaveType(int id)
    {
        NavigationManager.NavigateToDetailsLeaveType(id);
    }

    private async Task DeleteLeaveType(int id)
    {
        bool confirm = await Js.InvokeAsync<bool>("confirm", "Do you want to delete this leave type?");

        if(confirm)
        {
            Response<Guid> response = await LeaveTypeService.DeleteLeaveType(id);

            if (response.Success)
            {
                ToastService.ShowSuccess("Leave type deleted successfully");
                await OnInitializedAsync();
            }
            else
            {
                Message = response.Message;
            }
        }
    }
}