using CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;
using FluentValidation.TestHelper;

namespace CleanArch.Application.Tests.Features.LeaveTypes.Commands.UpdateLeaveTypes;

public class UpdateLeaveTypeCommandValidatorTest(UpdateLeaveTypeCommandValidatorFixture fixture)
    : IClassFixture<UpdateLeaveTypeCommandValidatorFixture>
{
    private readonly UpdateLeaveTypeCommandValidatorFixture _fixture = fixture;

    #region Tests

    [Fact]
    public async Task TestValidatorShouldFailWithNullId()
    {
        UpdateLeaveType.Command command = new(Guid.Empty, "name", 10);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage($"The {nameof(UpdateLeaveType.Command.Id)} is required.");
    }
    
    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        UpdateLeaveType.Command command = new(Guid.NewGuid(), "Test Vacation", 100);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion
}
