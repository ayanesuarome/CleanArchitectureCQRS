using AutoMapper;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Application.Models.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Api.Contracts.LeaveRequests;

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