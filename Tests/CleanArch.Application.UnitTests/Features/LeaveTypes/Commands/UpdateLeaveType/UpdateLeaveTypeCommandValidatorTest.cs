using CleanArch.Application.Features.LeaveTypes.Commands.UpdateLeaveType;
using FluentValidation.TestHelper;
using Moq;

namespace CleanArch.Application.UnitTests.Features.LeaveTypes.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidatorTest(UpdateLeaveTypeCommandValidatorFixture fixture)
    : IClassFixture<UpdateLeaveTypeCommandValidatorFixture>
{
    private readonly UpdateLeaveTypeCommandValidatorFixture _fixture = fixture;

    #region Tests

    [Fact]
    public async Task TestValidatorShouldFailWithNullId()
    {
        UpdateLeaveTypeCommand command = new();

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage($"{nameof(UpdateLeaveTypeCommand.Id)} is required");
    }

    [Fact]
    public async Task TestValidatorShouldFailWithNameLengthGreaterThan70()
    {
        UpdateLeaveTypeCommand command = new()
        {
            Name = "somenamesomenamesomenamesomenamesomenamesomenamesomenamesomenamesomename"
        };

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage($"{nameof(UpdateLeaveTypeCommand.Name)} must be up to 70 characters");
    }

    [Fact]
    public async Task TestValidatorShouldFailWithNameNotUnique()
    {
        UpdateLeaveTypeCommand command = new()
        {
            Name = "somename"
        };

        _fixture.repositoryMock
            .Setup(m => m.IsUniqueAsync(It.IsAny<string>(), default))
        .ReturnsAsync(false);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Leave type already exist");
    }

    [Fact]
    public async Task TestValidatorShouldNotFailWithNoNameToUpdate()
    {
        UpdateLeaveTypeCommand command = new()
        {
            Id = 1,
            DefaultDays = 100
        };

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        UpdateLeaveTypeCommand command = new()
        {
            Id = 1,
            Name = "somename",
            DefaultDays = 100
        };

        _fixture.repositoryMock
            .Setup(m => m.IsUniqueAsync(It.IsAny<string>(), default))
        .ReturnsAsync(true);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public async Task TestValidatorShouldFailWithDefaultDaysNotInRange1_100(int defaultDays)
    {
        UpdateLeaveTypeCommand command = new()
        {
            Name = "somename",
            DefaultDays = defaultDays
        };

        _fixture.repositoryMock
            .Setup(m => m.IsUniqueAsync(It.IsAny<string>(), default))
        .ReturnsAsync(true);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.DefaultDays)
            .WithErrorMessage("Default Days must be between 1 - 100");
    }

    #endregion
}
