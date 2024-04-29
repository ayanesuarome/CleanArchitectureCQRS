using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Identity.Services;

public class UserService(UserManager<ApplicationUser> userManager)
    : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Employee> GetEmployee(Guid userId)
    {
        ApplicationUser employee = await _userManager.FindByIdAsync(userId.ToString());

        return new Employee(
            new Guid(employee.Id),
            employee.Email,
            employee.FirstName.Value,
            employee.LastName.Value);
    }

    public async Task<List<Employee>> GetEmployees()
    {
        IList<ApplicationUser> employees = await _userManager.GetUsersInRoleAsync(Roles.Employee);

        return employees.Select(e => new Employee(
            new Guid(e.Id),
            e.Email,
            e.FirstName.Value,
            e.LastName.Value)).ToList();
    }
}
