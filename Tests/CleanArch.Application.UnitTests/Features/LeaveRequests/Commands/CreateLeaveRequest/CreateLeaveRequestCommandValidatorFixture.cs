using CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using CleanArch.Domain.Repositories;
using Moq;

namespace CleanArch.Application.Tests.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidatorFixture : IDisposable
{
    public CreateLeaveRequestCommandValidator validator;
    public Mock<ILeaveTypeRepository> repositoryMock;

    #region Setup and Cleanup

    public CreateLeaveRequestCommandValidatorFixture()
    {
        repositoryMock = new Mock<ILeaveTypeRepository>();
        validator = new CreateLeaveRequestCommandValidator(repositoryMock.Object);
    }

    public void Dispose()
    {
        repositoryMock = null;
        validator = null;
    }

    #endregion
}
