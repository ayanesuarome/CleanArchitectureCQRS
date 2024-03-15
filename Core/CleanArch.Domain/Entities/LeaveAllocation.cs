using System.ComponentModel.DataAnnotations;

namespace CleanArch.Domain.Entities;

public class LeaveAllocation : BaseEntity<int>
{
    public int NumberOfDays { get; private set; }
    public int Period { get; set; }
    public int LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }
    public string EmployeeId { get; set; } = null!;

    public bool HasEnoughDays(DateTimeOffset start, DateTimeOffset end)
    {
        return (int)(end - start).TotalDays < NumberOfDays;
    }

    public void UpdateNumberOfDays(int numberOfDays)
    {
        if(numberOfDays < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(NumberOfDays), $"Not enough number of days to take");
        }

        NumberOfDays = numberOfDays;        
    }
}
