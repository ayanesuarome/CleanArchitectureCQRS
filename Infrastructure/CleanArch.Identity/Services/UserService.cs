using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Identity.Services;

internal sealed class UserService(UserManager<User> userManager) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<Employee> GetEmployee(Guid userId)
    {
        User employee = await _userManager.FindByIdAsync(userId.ToString());

        return new Employee(
            new Guid(employee.Id),
            employee.Email,
            employee.FirstName.Value,
            employee.LastName.Value);
    }

    public async Task<List<Employee>> GetEmployees()
    {
        IList<User> employees = await _userManager.GetUsersInRoleAsync(Roles.Employee);

        return employees.Select(e => new Employee(
            new Guid(e.Id),
            e.Email,
            e.FirstName.Value,
            e.LastName.Value)).ToList();
    }
}
