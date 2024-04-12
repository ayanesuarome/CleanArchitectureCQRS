using CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;
using CleanArch.Application.Tests.Features.Mocks;
using CleanArch.Domain.Repositories;
using Moq;

namespace CleanArch.Application.UnitTests.Features.LeaveTypes.Commands.UpdateLeaveTypes;

public class UpdateLeaveTypeCommandValidatorFixture : IDisposable
{
    public UpdateLeaveType.Validator validator;
    public Mock<ILeaveTypeRepository> repositoryMock;

    public UpdateLeaveTypeCommandValidatorFixture()
    {
        repositoryMock = MockLeaveTypeRepository.GetLeaveTypeRepositoryMock();
        validator = new UpdateLeaveType.Validator(repositoryMock.Object);
    }

    public void Dispose()
    {
        repositoryMock = null;
        validator = null;
    }
}
