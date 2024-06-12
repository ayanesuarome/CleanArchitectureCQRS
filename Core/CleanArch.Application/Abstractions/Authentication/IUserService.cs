using CleanArch.Application.Contracts;

namespace CleanArch.Application.Abstractions.Authentication;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(Guid userId);
}
