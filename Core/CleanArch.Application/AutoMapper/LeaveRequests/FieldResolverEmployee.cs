using AutoMapper;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Models.Identity;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.AutoMapper.LeaveRequests;

public class FieldResolverEmployee(IUserService service) : IValueResolver<LeaveRequest, LeaveRequestDto, Employee>
{
    private readonly IUserService _service = service;

    public Employee Resolve(LeaveRequest source, LeaveRequestDto destination, Employee destMember, ResolutionContext context)
    {
        string employeeId = _service.UserId;
        return _service
            .GetEmployee(employeeId)
            .GetAwaiter()
            .GetResult();
    }
}
