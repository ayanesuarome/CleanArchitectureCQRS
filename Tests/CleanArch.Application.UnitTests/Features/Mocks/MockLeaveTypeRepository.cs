using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Core.ValueObjects;
using CleanArch.Domain.LeaveTypes;
using Moq;

namespace CleanArch.Application.Tests.Features.Mocks;

public static class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypeRepositoryMock()
    {
        LeaveTypeNameUniqueRequirement requirement = new(() => Task.FromResult(true));

        Result<Name> nameResult1 = Name.Create("Test Vacation");
        Result<Name> nameResult2 = Name.Create("Test Sick");
        Result<Name> nameResult3 = Name.Create("Test Maternity");
        Result<DefaultDays> defaultDaysResult1 = DefaultDays.Create(10);
        Result<DefaultDays> defaultDaysResult2 = DefaultDays.Create(15);
        Result<DefaultDays> defaultDaysResult3 = DefaultDays.Create(30);

        List<LeaveType> leaveTypes = new()
        {
            LeaveType.Create(nameResult1.Value, defaultDaysResult1.Value, requirement).Value,
            LeaveType.Create(nameResult2.Value, defaultDaysResult1.Value, requirement).Value,
            LeaveType.Create(nameResult3.Value, defaultDaysResult1.Value, requirement).Value
        };

        Mock<ILeaveTypeRepository> repositoryMock = new Mock<ILeaveTypeRepository>();

        repositoryMock
            .Setup(m => m.GetAsync())
            .ReturnsAsync(leaveTypes);

        repositoryMock
            .Setup(m => m.GetByIdAsync(It.IsAny<LeaveTypeId>()))
            .ReturnsAsync(leaveTypes[0]);

        repositoryMock
            .Setup(m => m.Add(It.IsAny<LeaveType>()));

        return repositoryMock;
    }
}
