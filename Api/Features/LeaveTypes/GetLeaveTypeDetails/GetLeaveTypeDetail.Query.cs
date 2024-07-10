using CleanArch.Application.Abstractions.Messaging;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeDetails;

public static partial class GetLeaveTypeDetail
{
    public sealed record Query(Guid Id) : ICachedQuery<Response>
    {
        public string CacheKey => $"leave-type-by-id-{Id}";

        public TimeSpan? Expiration => null;
    }
}
