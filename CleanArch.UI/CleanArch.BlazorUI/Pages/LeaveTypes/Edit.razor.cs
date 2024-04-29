using Blazored.Toast.Services;
using CleanArch.BlazorUI.Extensions;
using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.LeaveTypes
{
    public partial class Edit
    {
        [Parameter] public Guid Id { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ILeaveTypeService Service { get; set; } = null!;
        [Inject] private IToastService ToastService { get; set; } = null!;

        private string? Message { get; set; }
        private LeaveTypeVM Model { get; set; } = new();

        protected override async Task OnParametersSetAsync()
        {
            Model = await Service.GetLeaveTypeDetails(Id);
        }

        private async Task EditLeaveTypeAsync()
        {
            var response = await Service.UpdateLeaveType(Model);
            
            if (response.Success)
            {
                ToastService.ShowSuccess("Leave type updated successfully");
                NavigationManager.NavigateToIndexLeaveType();
            }
            else
            {
                Message = response.Message;
            }
        }
    }
}