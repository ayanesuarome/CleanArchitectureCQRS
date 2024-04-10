using AutoMapper;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Domain.Entities;

namespace CleanArch.Api.Features.LeaveRequests;

public class FieldResolverUserId(IUserService service) : IValueResolver<CreateLeaveRequests.CreateLeaveRequest.Command, LeaveRequest, string>
{
    private readonly IUserService _service = service;

    public string Resolve(
        CreateLeaveRequests.CreateLeaveRequest.Command source,
        LeaveRequest destination,
        string destMember, ResolutionContext context)
    {
        return _service.UserId;
    }
}