using AutoMapper;
using CleanArch.Contracts.LeaveTypes;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public static partial class GetLeaveTypeList
{
    public sealed class Handler(IMapper mapper, ILeaveTypeRepository repository)
        : IRequestHandler<Query, Result<LeaveTypeListDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = repository;

        public async Task<Result<LeaveTypeListDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<LeaveType> leaveTypes = await _repository.GetAsync();

            LeaveTypeListDto leaveTypeDtos = new(
                _mapper.Map<IReadOnlyCollection<LeaveTypeListDto.LeaveTypeModel>>(leaveTypes));

            return Result.Success(leaveTypeDtos);
        }
    }
}
