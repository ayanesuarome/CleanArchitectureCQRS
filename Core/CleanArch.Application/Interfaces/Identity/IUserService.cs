using CleanArch.Application.Models.Identity;

namespace CleanArch.Application.Interfaces.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(string userId);
    public string UserId { get; }
}
