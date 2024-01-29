using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.LeaveTypes;

public partial class Create
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private ILeaveTypeService LeaveTypeService { get; set; } = null!;
    
    private string? Message { get; set; }

    private LeaveTypeVM Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = new();
    }

    private async Task CreateLeaveType()
    {
        Response<Guid> result = await LeaveTypeService.CreateLeaveType(Model);

        if (result.Success)
        {
            NavigationManager.NavigateToIndexLeaveType();
        }
        else
        {
            Message = result.Message;
        }
    }
}