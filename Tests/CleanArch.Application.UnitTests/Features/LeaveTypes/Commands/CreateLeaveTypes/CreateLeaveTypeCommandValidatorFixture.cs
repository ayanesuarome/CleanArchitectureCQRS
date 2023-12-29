using CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using CleanArch.Domain.Interfaces.Persistence;
using Moq;

namespace CleanArch.Application.UnitTests.Features.LeaveTypes.Commands.CreateLeaveTypes;

public class CreateLeaveTypeCommandValidatorFixture : IDisposable
{
    public CreateLeaveTypeCommandValidator validator;
    public Mock<ILeaveTypeRepository> repositoryMock;

    #region Setup and Cleanup

    public CreateLeaveTypeCommandValidatorFixture()
    {
        repositoryMock = new Mock<ILeaveTypeRepository>();
        validator = new CreateLeaveTypeCommandValidator(repositoryMock.Object);
    }

    public void Dispose()
    {
        repositoryMock = null;
        validator = null;
    }

    #endregion
}