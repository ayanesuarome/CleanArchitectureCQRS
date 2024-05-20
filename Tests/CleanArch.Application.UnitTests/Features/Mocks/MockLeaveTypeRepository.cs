using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.Repositories;
using CleanArch.Domain.ValueObjects;
using Moq;

namespace CleanArch.Application.Tests.Features.Mocks;

public static class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypeRepositoryMock()
    {
        Result<Name> nameResult1 = Name.Create("Test Vacation");
        Result<Name> nameResult2 = Name.Create("Test Sick");
        Result<Name> nameResult3 = Name.Create("Test Maternity");
        Result<DefaultDays> defaultDaysResult1 = DefaultDays.Create(10);
        Result<DefaultDays> defaultDaysResult2 = DefaultDays.Create(15);
        Result<DefaultDays> defaultDaysResult3 = DefaultDays.Create(30);

        List<LeaveType> leaveTypes = new()
        {
            new(nameResult1.Value, defaultDaysResult1.Value),
            new(nameResult2.Value, defaultDaysResult1.Value),
            new(nameResult3.Value, defaultDaysResult1.Value)
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
