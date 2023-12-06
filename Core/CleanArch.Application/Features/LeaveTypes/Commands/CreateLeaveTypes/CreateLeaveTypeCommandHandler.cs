using AutoMapper;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces.Persistence;
using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveTypes
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _repository;

        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            LeaveType leaveTypeToCreate = _mapper.Map<LeaveType>(request);
            await _repository.CreateAsync(leaveTypeToCreate);

            return leaveTypeToCreate.Id;
        }
    }
}
