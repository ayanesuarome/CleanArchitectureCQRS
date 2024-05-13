using CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;
using CleanArch.Domain.Repositories;
using Moq;

namespace CleanArch.Application.Tests.Features.LeaveTypes.Commands.CreateLeaveTypes;

public class CreateLeaveTypeCommandValidatorFixture : IDisposable
{
    public CreateLeaveType.Validator validator;
    public Mock<ILeaveTypeRepository> repositoryMock;

    #region Setup and Cleanup

    public CreateLeaveTypeCommandValidatorFixture()
    {
        repositoryMock = new Mock<ILeaveTypeRepository>();
        validator = new CreateLeaveType.Validator(repositoryMock.Object);
    }

    public void Dispose()
    {
        repositoryMock = null;
        validator = null;
    }

    #endregion
}