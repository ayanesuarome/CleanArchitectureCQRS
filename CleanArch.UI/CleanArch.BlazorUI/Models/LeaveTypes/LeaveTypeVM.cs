using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArch.BlazorUI.Models.LeaveTypes;

public class LeaveTypeVM
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [DisplayName("Default Number Of Days")]
    public int DefaultDays { get; set; }
}
