using AutoMapper;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Contracts.Identity;

namespace CleanArch.Api.Features.LeaveRequests;

public class FieldResolverEmployeeForLeaveRequestDetailsDto(IUserService service) : IValueResolver<LeaveRequest, LeaveRequestDetailsDto, Employee>
{
    private readonly IUserService _service = service;

    public Employee Resolve(LeaveRequest source, LeaveRequestDetailsDto destination, Employee destMember, ResolutionContext context)
    {
        return _service
            .GetEmployee(source.RequestingEmployeeId)
            .GetAwaiter()
            .GetResult();
    }
}