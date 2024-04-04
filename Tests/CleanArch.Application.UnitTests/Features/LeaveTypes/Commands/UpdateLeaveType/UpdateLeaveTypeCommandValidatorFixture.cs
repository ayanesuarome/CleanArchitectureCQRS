using CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveType;
using CleanArch.Application.Tests.Features.Mocks;
using CleanArch.Domain.Repositories;
using Moq;

namespace CleanArch.Application.UnitTests.Features.LeaveTypes.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidatorFixture : IDisposable
{
    public UpdateLeaveTypeCommandValidator validator;
    public Mock<ILeaveTypeRepository> repositoryMock;

    public UpdateLeaveTypeCommandValidatorFixture()
    {
        repositoryMock = MockLeaveTypeRepository.GetLeaveTypeRepositoryMock();
        validator = new UpdateLeaveTypeCommandValidator(repositoryMock.Object);
    }

    public void Dispose()
    {
        repositoryMock = null;
        validator = null;
    }
}
