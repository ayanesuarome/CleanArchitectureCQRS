using CleanArch.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace CleanArch.BlazorUI.Pages.LeaveTypes
{
    public partial class FormComponent
    {
        [Parameter] public bool Disabled { get; set; } = false;
        [Parameter] public LeaveTypeVM LeaveType { get; set; } = null!;
        [Parameter] public string ButtonText { get; set; } = "Save";
        [Parameter] public EventCallback OnValidSubmit { get; set; }
    }
}