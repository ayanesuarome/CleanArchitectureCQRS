using System.Text;

namespace CleanArch.Application.Models.Identity;

public class Employee
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string GetName() {
        StringBuilder name = new(FirstName);

        if (LastName != null)
        {
            name.Append(" ")
                .Append(LastName);
        }

        return name.ToString();
    }
}