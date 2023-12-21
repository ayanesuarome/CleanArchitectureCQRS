using MediatR;

namespace CleanArch.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocations;

public class CreateLeaveAllocationCommand : IRequest<int>
{
    public int NumberOfDays { get; set; }
    public int Period { get; set; }
    public int LeaveTypeId { get; set; }
    public string EmployeeId { get; set; } = null!;
}
