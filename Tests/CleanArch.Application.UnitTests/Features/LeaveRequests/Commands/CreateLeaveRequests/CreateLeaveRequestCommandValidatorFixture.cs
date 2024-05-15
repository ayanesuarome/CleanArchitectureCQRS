using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;

namespace CleanArch.Application.Tests.Features.LeaveRequests.Commands.CreateLeaveRequests;

public class CreateLeaveRequestCommandValidatorFixture : IDisposable
{
    internal CreateLeaveRequest.Validator validator;

    #region Setup and Cleanup

    public CreateLeaveRequestCommandValidatorFixture()
    {
        validator = new CreateLeaveRequest.Validator();
    }

    public void Dispose()
    {
        validator = null;
    }

    #endregion
}
