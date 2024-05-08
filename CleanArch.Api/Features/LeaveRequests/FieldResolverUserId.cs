using AutoMapper;
using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Domain.Entities;

namespace CleanArch.Api.Features.LeaveRequests;

public class FieldResolverUserId(IUserIdentifierProvider userIdentifierProvider)
    : IValueResolver<CreateLeaveRequests.CreateLeaveRequest.Command, LeaveRequest, string>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider = userIdentifierProvider;

    public string Resolve(
        CreateLeaveRequests.CreateLeaveRequest.Command source,
        LeaveRequest destination,
        string destMember, ResolutionContext context)
    {
        return _userIdentifierProvider.UserId.ToString();
    }
}