using AutoMapper;
using CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using CleanArch.Application.Interfaces.Identity;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.AutoMapper.LeaveRequests;

public class FieldResolverUserId(IUserService service) : IValueResolver<CreateLeaveRequestCommand, LeaveRequest, string>
{
    private readonly IUserService _service = service;

    public string Resolve(CreateLeaveRequestCommand source, LeaveRequest destination, string destMember, ResolutionContext context)
    {
        return _service.UserId;
    }
}