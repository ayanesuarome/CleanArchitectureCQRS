using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CleanArch.BlazorUI.Pages.LeaveTypes;

public partial class Index
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private ILeaveTypeService LeaveTypeService { get; set; } = null!;
    
    private List<LeaveTypeVM> LeaveTypes { get; set; } = null!;

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
        LeaveTypeVM leaveType = await LeaveTypeService.GetLeaveTypeDetails(id);
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
        Response<Guid> response = await LeaveTypeService.DeleteLeaveType(id);

        if(response.Success)
        {
            StateHasChanged();
        }
        else
        {
            Message = response.Message;
        }
    }
}