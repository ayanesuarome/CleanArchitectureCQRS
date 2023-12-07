using MediatR;

namespace CleanArch.Application.Features.LeaveTypes.Commands.DeleteLeaveTypes;

public record DeleteLeaveTypeCommand(int Id) : IRequest<Unit>;
