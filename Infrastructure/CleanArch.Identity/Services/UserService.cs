using CleanArch.Application.Abstractions.Identity;
using CleanArch.Application.Models.Identity;
using CleanArch.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Identity.Services;

public class UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;

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
        IList<ApplicationUser> employees = await _userManager.GetUsersInRoleAsync(Roles.Employee);

        return employees.Select(e => new Employee
        {
            Id = e.Id,
            Email = e.Email,
            FirstName = e.FirstName,
            LastName = e.LastName,
        }).ToList();
    }
}
