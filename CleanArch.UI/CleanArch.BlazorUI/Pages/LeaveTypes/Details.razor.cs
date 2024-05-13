using CleanArch.BlazorUI.Interfaces;
using CleanArch.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.LeaveTypes
{
    public partial class Details
    {
        [Parameter] public Guid Id { get; set; }

        [Inject] private ILeaveTypeService Service { get; set; } = null!;

        private LeaveTypeVM Model { get; set; } = new();

        protected override async Task OnParametersSetAsync()
        {
            Model = await Service.GetLeaveTypeDetails(Id);
        }
    }
}