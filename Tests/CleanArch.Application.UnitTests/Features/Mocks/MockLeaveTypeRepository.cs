using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using Moq;

namespace CleanArch.Application.Tests.Features.Mocks;

public static class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypeRepositoryMock()
    {
        List<LeaveType> leaveTypes = new()
        {
            new("Test Vacation", 10)
            {
                Id = 1
            },
            new("Test Sick", 15)
            {
                Id = 2,
            },
            new("Test Maternity", 15)
            {
                Id = 3,
            }
        };

        Mock<ILeaveTypeRepository> repositoryMock = new Mock<ILeaveTypeRepository>();

        repositoryMock
            .Setup(m => m.GetAsync())
            .ReturnsAsync(leaveTypes);

        repositoryMock
            .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(leaveTypes[0]);

        repositoryMock
            .Setup(m => m.CreateAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType LeaveType) =>
            {
                leaveTypes.Add(LeaveType);
                return Task.CompletedTask;
            });

        return repositoryMock;
    }
}
