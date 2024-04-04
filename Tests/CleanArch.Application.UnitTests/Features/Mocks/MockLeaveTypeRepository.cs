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
            new()
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            },
            new()
            {
                Id = 2,
                DefaultDays = 15,
                Name = "Test Sick"
            },
            new()
            {
                Id = 3,
                DefaultDays = 15,
                Name = "Test Maternity"
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
