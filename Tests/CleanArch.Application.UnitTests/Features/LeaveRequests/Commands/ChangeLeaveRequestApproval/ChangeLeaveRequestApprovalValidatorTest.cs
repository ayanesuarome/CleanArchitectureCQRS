using CleanArch.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;
using FluentValidation.TestHelper;

namespace CleanArch.Application.UnitTests.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalValidatorTest
{
    private readonly ChangeLeaveRequestApprovalCommandValidator _validator;

    public ChangeLeaveRequestApprovalValidatorTest()
    {
        _validator = new ChangeLeaveRequestApprovalCommandValidator();
    }

    [Fact]
    public async Task TestValidatorShouldFailWitInvalidId()
    {
        ChangeLeaveRequestApprovalCommand command = new()
        {
            Id = 0
        };

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage($"{nameof(ChangeLeaveRequestApprovalCommand.Id)} is required");
    }

    [Fact]
    public async Task TestValidatorShouldFailWithNullApproved()
    {
        ChangeLeaveRequestApprovalCommand command = new()
        {
            Id = 1
        };

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Approved)
            .WithErrorMessage("Approval status cannot be null");
    }
}
