using CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;

namespace CleanArch.Application.Tests.Features.LeaveTypes.Commands.CreateLeaveTypes;

public class CreateLeaveTypeCommandValidatorFixture : IDisposable
{
    internal CreateLeaveType.Validator validator;

    #region Setup and Cleanup

    public CreateLeaveTypeCommandValidatorFixture()
    {
        validator = new CreateLeaveType.Validator();
    }

    public void Dispose()
    {
        validator = null;
    }

    #endregion
}