using CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;
using FluentValidation.TestHelper;

namespace CleanArch.Application.Tests.Features.LeaveRequests.Commands.ChangeLeaveRequestApprovals;

public class ChangeLeaveRequestApprovalValidatorTest
{
    private readonly ChangeLeaveRequestApproval.Validator _validator;

    public ChangeLeaveRequestApprovalValidatorTest()
    {
        _validator = new ChangeLeaveRequestApproval.Validator();
    }

    [Fact]
    public async Task TestValidatorShouldFailWitInvalidId()
    {
        ChangeLeaveRequestApproval.Command command = new(false)
        {
            Id = 0
        };

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage("The Id is required.");
    }
}
