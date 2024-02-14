using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveRequests;
using CleanArch.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.LeaveRequests;

public partial class Details
{
    [Inject] private ILeaveRequestService Service { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Parameter] public int Id { get; set; }
    
    private string ClassName;
    private string HeadingText;

    private LeaveRequestVM Model { get; set; } = new();
    private string? Message { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Model = await Service.GetLeaveRequestAsync(Id);
    }

    protected override void OnInitialized()
    {
        switch (Model.IsApproved)
        {
            case true:
                ClassName = "success";
                HeadingText = "Approved";
                break;
            case false:
                ClassName = "danger";
                HeadingText = "Rejected";
                break;
            default:
                ClassName = "warning";
                HeadingText = "Pending Approval";
                break;
        }
    }

    private async Task ChangeApproval(bool status)
    {
        Response<Guid> response = await Service.ApprovedLeaveRequestAsync(Id, status);

        if(response.Success)
        {
            NavigationManager.NavigateToIndexLeaveRequest();
        }
        else
        {
            Message = response.Message;
        }
    }
}