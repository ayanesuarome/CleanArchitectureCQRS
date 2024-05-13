using CleanArch.Contracts.Identity;

namespace CleanArch.Application.Abstractions.Authentication;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(Guid userId);
}
