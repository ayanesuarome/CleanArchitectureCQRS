using AutoMapper;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.Identity;
using CleanArch.Contracts.LeaveRequests;
using CleanArch.Domain.Entities;

namespace CleanArch.Api.Features.LeaveRequests;

public class FieldResolverEmployeeForLeaveRequestDto(IUserService service) : IValueResolver<LeaveRequest, LeaveRequestDetailsDto, Employee>
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