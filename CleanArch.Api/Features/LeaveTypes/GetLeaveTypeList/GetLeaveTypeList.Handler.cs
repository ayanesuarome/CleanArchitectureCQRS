using AutoMapper;
using CleanArch.Api.Contracts.LeaveTypes;
using CleanArch.Application.ResultPattern;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveTypes.GetLeaveTypeList;

public static partial class GetLeaveTypeList
{
    internal sealed class Handler(IMapper mapper, ILeaveTypeRepository repository)
        : IRequestHandler<Query, Result<LeaveTypeListDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = repository;

        public async Task<Result<LeaveTypeListDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<LeaveType> leaveTypes = await _repository.GetAsync();
            LeaveTypeListDto leaveTypeDtos = new(
                _mapper.Map<IReadOnlyCollection<LeaveTypeListDto.LeaveTypeModel>>(leaveTypes));

            return Result.Success(leaveTypeDtos);
        }
    }
}
