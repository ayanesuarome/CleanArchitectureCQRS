using CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveType;
using CleanArch.Domain.Interfaces.Persistence;
using Moq;

namespace CleanArch.Application.UnitTests.Features.LeaveTypes.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidatorFixture : IDisposable
{
    public UpdateLeaveTypeCommandValidator validator;
    public Mock<ILeaveTypeRepository> repositoryMock;

    public UpdateLeaveTypeCommandValidatorFixture()
    {
        repositoryMock = new Mock<ILeaveTypeRepository>();
        validator = new UpdateLeaveTypeCommandValidator(repositoryMock.Object);
    }

    public void Dispose()
    {
        repositoryMock = null;
        validator = null;
    }
}
