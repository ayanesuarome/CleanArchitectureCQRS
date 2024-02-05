using CleanArch.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using FluentValidation.TestHelper;

namespace CleanArch.Application.UnitTests.Features.LeaveRequests.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestValidatorTest(UpdateLeaveRequestValidatorFixture fixture)
    : IClassFixture<UpdateLeaveRequestValidatorFixture>
{
    private readonly UpdateLeaveRequestValidatorFixture _fixture = fixture;

    [Fact]
    public async Task TestValidatorShouldFailWithInvalidId()
    {
        UpdateLeaveRequestCommand command = new()
        {
            Id = 0,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage($"{nameof(UpdateLeaveRequestCommand.Id)} is required");
    }
    
    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        UpdateLeaveRequestCommand command = new()
        {
            Id = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
