using AutoMapper;
using CleanArch.Application.Features.LeaveRequests.Queries.Shared;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Application.Models.Identity;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.AutoMapper.LeaveRequests;

public class FieldResolverEmployeeForLeaveRequestDto(IUserService service) : IValueResolver<LeaveRequest, LeaveRequestDto, Employee>
{
    private readonly IUserService _service = service;

    public Employee Resolve(LeaveRequest source, LeaveRequestDto destination, Employee destMember, ResolutionContext context)
    {
        return _service
            .GetEmployee(source.RequestingEmployeeId)
            .GetAwaiter()
            .GetResult();
    }
}