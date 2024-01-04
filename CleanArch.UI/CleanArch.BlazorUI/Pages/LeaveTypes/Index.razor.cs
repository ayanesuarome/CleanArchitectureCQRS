using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CleanArch.BlazorUI.Pages.LeaveTypes;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }
    public List<LeaveTypeVM> LeaveTypes { get; private set; }
    public string Message { get; set; } = null!;

    private void CreateLeaveType(MouseEventArgs e)
    {
        NavigationManager.NavigateTo(LeaveTypePaths.CreateLeaveType);
    }

    private async Task AllocateLeaveType(int id)
    {
        LeaveTypeVM leaveType = await LeaveTypeService.GetLeaveTypeDetails(id);
    }

    private void EditLeaveType(int id)
    {
        NavigationManager.NavigateTo(String.Format(LeaveTypePaths.EditLeaveType, id));
    }
    
    private void DetailsLeaveType(int id)
    {
        NavigationManager.NavigateTo(String.Format(LeaveTypePaths.DetailsLeaveType, id));
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

    protected override async Task OnInitializedAsync()
    {
        LeaveTypes = await LeaveTypeService.GetLeaveTypeList();
    }
}