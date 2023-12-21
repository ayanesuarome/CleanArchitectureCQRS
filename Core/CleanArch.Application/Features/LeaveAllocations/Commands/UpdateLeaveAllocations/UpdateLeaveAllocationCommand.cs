using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocations;

public class UpdateLeaveAllocationCommand : IRequest
{
    public int Id { get; set; }
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public int LeaveTypeId { get; set; }
}
