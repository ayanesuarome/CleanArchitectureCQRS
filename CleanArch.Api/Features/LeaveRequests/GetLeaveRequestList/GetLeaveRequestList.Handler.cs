using AutoMapper;
using CleanArch.Api.Contracts.LeaveRequests;
using CleanArch.Application.Abstractions.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using MediatR;

namespace CleanArch.Api.Features.LeaveRequests.GetLeaveRequestList;

public static partial class GetLeaveRequestList
{
    internal sealed class Handler(IMapper mapper, ILeaveRequestRepository repository, IUserService userService)
    : IRequestHandler<Query, Result<LeaveRequestListDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveRequestRepository _repository = repository;
        private readonly IUserService _userService = userService;

        public async Task<Result<LeaveRequestListDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            string userId = _userService.UserId;

            IReadOnlyCollection<LeaveRequest> leaveRequests = await _repository.GetLeaveRequestsWithDetailsAsync(userId);

            LeaveRequestListDto requestListDto = new(
                _mapper.Map<IReadOnlyCollection<LeaveRequestDetailsDto>>(leaveRequests));

            return new SuccessResult<LeaveRequestListDto>(requestListDto);
        }
    }
}
