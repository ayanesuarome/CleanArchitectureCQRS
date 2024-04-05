using AutoMapper;
using CleanArch.Application.Features.LeaveRequests.Queries.GetLeaveRequestDetails;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Application.Models.Identity;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.AutoMapper.LeaveRequests;

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