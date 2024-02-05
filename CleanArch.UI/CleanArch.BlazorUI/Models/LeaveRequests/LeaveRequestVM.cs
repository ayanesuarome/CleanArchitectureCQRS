using CleanArch.BlazorUI.Models.Identity;
using CleanArch.BlazorUI.Models.LeaveTypes;
using System.ComponentModel.DataAnnotations;

namespace CleanArch.BlazorUI.Models.LeaveRequests;

public class LeaveRequestVM
{
    public int Id { get; set; }

    [Display(Name = "Start Date")]
    [Required]
    public DateTimeOffset? StartDate { get; set; }
    
    [Display(Name = "End Date")]
    [Required]
    public DateTimeOffset? EndDate { get; set; }

    [Required]
    public int LeaveTypeId { get; set; }
    public LeaveTypeVM LeaveType { get; set; } = new();

    [Display(Name = "Date Requested")]
    public DateTimeOffset DateRequested { get; set; }

    [Display(Name = "Comments")]
    [MaxLength(300)]
    public string? RequestComments { get; set; }
    
    [Display(Name = "Approval State")]
    public bool? IsApproved { get; set; }
    public bool IsCancelled { get; set; }
    public EmployeeVM Employee { get; set; } = new();
    public DateTimeOffset DateActioned { get; set; }
}
