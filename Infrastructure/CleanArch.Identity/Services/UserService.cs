using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Identity.Services;

public class UserService(UserManager<ApplicationUser> userManager)
    : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Employee> GetEmployee(string userId)
    {
        ApplicationUser employee = await _userManager.FindByIdAsync(userId);

        return new Employee
        {
            Id = employee.Id,
            Email = employee.Email,
            FirstName = employee.FirstName.Value,
            LastName = employee.LastName.Value,
        };
    }

    public async Task<List<Employee>> GetEmployees()
    {
        IList<ApplicationUser> employees = await _userManager.GetUsersInRoleAsync(Roles.Employee);

        return employees.Select(e => new Employee
        {
            Id = e.Id,
            Email = e.Email,
            FirstName = e.FirstName.Value,
            LastName = e.LastName.Value,
        }).ToList();
    }
}
