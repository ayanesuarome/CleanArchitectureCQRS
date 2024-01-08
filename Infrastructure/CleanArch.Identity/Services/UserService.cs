using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Models.Identity;
using CleanArch.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Identity.Services;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public string UserId => throw new NotImplementedException();

    // TODO: move to a static class
    private const string EmployeeRole = "Employee";

    public async Task<Employee> GetEmployee(string userId)
    {
        ApplicationUser employee = await _userManager.FindByIdAsync(userId);

        return new Employee
        {
            Id = employee.Id,
            Email = employee.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
        };
    }

    public async Task<List<Employee>> GetEmployees()
    {
        IList<ApplicationUser> employees = await _userManager.GetUsersInRoleAsync(EmployeeRole);

        return employees.Select(e => new Employee
        {
            Id = e.Id,
            Email = e.Email,
            FirstName = e.FirstName,
            LastName = e.LastName,
        }).ToList();
    }
}
