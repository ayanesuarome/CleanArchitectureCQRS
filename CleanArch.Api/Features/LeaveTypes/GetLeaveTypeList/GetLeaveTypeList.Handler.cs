using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public static partial class GetLeaveTypeList
{
    public sealed class Handler(ILeaveTypeRepository repository)
        : IQueryHandler<Query, Result<LeaveTypeListDto>>
    {
        private readonly ILeaveTypeRepository _repository = repository;

        public async Task<Result<LeaveTypeListDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<LeaveType> leaveTypes = await _repository.GetAsync();

            LeaveTypeListDto.LeaveTypeModel[] listModel = leaveTypes.Select(leave =>
                new LeaveTypeListDto.LeaveTypeModel(
                    leave.Id,
                    leave.Name.Value,
                    leave.DefaultDays.Value))
                .ToArray();

            LeaveTypeListDto leaveTypeDtos = new(listModel);

            return Result.Success(leaveTypeDtos);
        }
    }
}
