using System.Text;

namespace CleanArch.BlazorUI.Models.Identity;

public class EmployeeVM
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string  FullName()
    {
        StringBuilder name = new(FirstName);
        
        if(LastName != null)
        {
            name.Append(' ')
                .Append(LastName);
        }

        return name.ToString();
    }
}
