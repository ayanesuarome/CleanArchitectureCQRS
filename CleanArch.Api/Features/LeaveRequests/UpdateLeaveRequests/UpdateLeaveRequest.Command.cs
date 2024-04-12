using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;

public static partial class UpdateLeaveRequest
{
    public sealed record Command
        : BaseCommand, IRequest<Result<LeaveRequest>>
    {
        public int Id { get; set; }
        public string? RequestComments { get; set; }
        public bool IsCancelled { get; set; }
    }
}
