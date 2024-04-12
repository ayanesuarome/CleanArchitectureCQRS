using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Domain.Repositories;
using Moq;

namespace CleanArch.Application.Tests.Features.LeaveRequests.Commands.CreateLeaveRequests;

public class CreateLeaveRequestCommandValidatorFixture : IDisposable
{
    public CreateLeaveRequest.Validator validator;
    public Mock<ILeaveTypeRepository> repositoryMock;

    #region Setup and Cleanup

    public CreateLeaveRequestCommandValidatorFixture()
    {
        repositoryMock = new Mock<ILeaveTypeRepository>();
        validator = new CreateLeaveRequest.Validator(repositoryMock.Object);
    }

    public void Dispose()
    {
        repositoryMock = null;
        validator = null;
    }

    #endregion
}
