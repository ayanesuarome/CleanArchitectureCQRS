namespace CleanArch.BlazorUI.Models.Identity;

internal sealed class EmployeeVM
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string  FullName { get; set; } = null!;
}
