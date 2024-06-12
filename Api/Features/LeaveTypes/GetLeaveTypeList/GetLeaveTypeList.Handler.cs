using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.LeaveTypes;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public static partial class GetLeaveTypeList
{
    internal sealed class Handler(ILeaveTypeRepository repository) : IQueryHandler<Query, Response>
    {
        private readonly ILeaveTypeRepository _repository = repository;

        public async Task<Result<Response>> Handle(Query query, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<LeaveType> leaveTypes = await _repository.GetAsync();

            Response.Model[] listModel = leaveTypes.Select(leave =>
                new Response.Model(
                    leave.Id,
                    leave.Name.Value,
                    leave.DefaultDays.Value))
                .ToArray();

            Response leaveTypeDtos = new(listModel);

            return Result.Success(leaveTypeDtos);
        }
    }
}
