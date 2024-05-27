using System.ComponentModel.DataAnnotations;

namespace CleanArch.BlazorUI.Models.LeaveRequests;

internal sealed class LeaveRequestVM
{
    public Guid Id { get; set; }

    [Display(Name = "Start Date")]
    [Required]
    public DateOnly StartDate { get; set; }
    
    [Display(Name = "End Date")]
    [Required]
    public DateOnly EndDate { get; set; }

    [Required]
    public Guid LeaveTypeId { get; set; }

    public string LeaveTypeName { get; set; }

    public string EmployeeFullName { get; set; }

    [Display(Name = "Date Requested")]
    public DateTimeOffset DateCreated { get; set; }

    [Display(Name = "Comments")]
    [MaxLength(300)]
    public string? RequestComments { get; set; }
    
    [Display(Name = "Approval State")]
    public bool? IsApproved { get; set; }
    public bool IsCancelled { get; set; }
}
