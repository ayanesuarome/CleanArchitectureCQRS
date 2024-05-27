using Blazored.Toast.Services;
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
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private IConfiguration Configuration { get; set; } = null!;

    [Parameter] public Guid Id { get; set; }
    
    private string? ClassName;
    private string? HeadingText;

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
                ClassName = Configuration.GetValue<string>("ApprovedClassName");
                HeadingText = "Approved";
                break;
            case false:
                ClassName = Configuration.GetValue<string>("RejectedClassName");
                HeadingText = "Rejected";
                break;
            default:
                ClassName = Configuration.GetValue<string>("PendingClassName");
                HeadingText = "Pending Approval";
                break;
        }
    }

    private async Task ChangeApproval(bool status)
    {
        Response<Guid> response = await Service.ApprovedLeaveRequestAsync(Id, status);

        if(response.Success)
        {
            ToastService.ShowSuccess("Leave request approval updated");
            NavigationManager.NavigateToIndexLeaveRequest();
        }
        else
        {
            Message = response.Message;
        }
    }
}