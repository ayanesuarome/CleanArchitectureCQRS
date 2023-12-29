using CleanArch.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using FluentValidation.TestHelper;
using Moq;

namespace CleanArch.Application.UnitTests.Features.LeaveTypes.Commands.CreateLeaveTypes;

public class CreateLeaveTypeCommandValidatorTest(CreateLeaveTypeCommandValidatorFixture fixture)
    : IClassFixture<CreateLeaveTypeCommandValidatorFixture>
{
    private readonly CreateLeaveTypeCommandValidatorFixture _fixture = fixture;

    #region Tests

    [Theory]
    [MemberData(nameof(GetInvalidStrings))]
    public void TestValidatorShouldFailWithNullOrEmptyName(string name)
    {
        CreateLeaveTypeCommand command = new()
        {
            Name = name
        };

        var result = _fixture.validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage($"{nameof(CreateLeaveTypeCommand.Name)} is required");
    }

    [Fact]
    public void TestValidatorShouldFailWithNameLengthGreaterThan70()
    {
        CreateLeaveTypeCommand command = new()
        {
            Name = "somenamesomenamesomenamesomenamesomenamesomenamesomenamesomenamesomename"
        };

        var result = _fixture.validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage($"{nameof(CreateLeaveTypeCommand.Name)} must be up to 70 characters");
    }

    [Fact]
    public async Task TestValidatorShouldFailWithNameNotUnique()
    {
        CreateLeaveTypeCommand command = new()
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

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public async Task TestValidatorShouldFailWithDefaultDaysNotInRange1_100(int defaultDays)
    {
        CreateLeaveTypeCommand command = new()
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

    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        CreateLeaveTypeCommand command = new()
        {
            Name = "somename",
            DefaultDays = 100
        };

        _fixture.repositoryMock
            .Setup(m => m.IsUniqueAsync(It.IsAny<string>(), default))
        .ReturnsAsync(true);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion

    #region Member Test Data

    public static IEnumerable<object[]> GetInvalidStrings()
    {
        yield return new object[] { null };
        yield return new object[] { string.Empty };
        yield return new object[] { " " };
    }

    #endregion
}
