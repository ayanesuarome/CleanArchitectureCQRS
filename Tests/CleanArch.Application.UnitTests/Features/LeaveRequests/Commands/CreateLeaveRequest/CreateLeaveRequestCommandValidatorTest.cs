using CleanArch.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using CleanArch.Domain.Entities;
using FluentValidation.TestHelper;
using Moq;

namespace CleanArch.Application.Tests.Features.LeaveRequests.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidatorTest(CreateLeaveRequestCommandValidatorFixture fixture)
    : IClassFixture<CreateLeaveRequestCommandValidatorFixture>
{
    private readonly CreateLeaveRequestCommandValidatorFixture _fixture = fixture;
    
    #region Tests

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task TestValidatorShouldFailWithLeaveTypeIdLessThanOrEqualTo0(int leaveTypeId)
    {
        CreateLeaveRequestCommand command = new()
        {
            LeaveTypeId = leaveTypeId
        };

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.LeaveTypeId)
            .WithErrorMessage("Leave Type Id should be greather than 0");
    }

    [Fact]
    public async Task TestValidatorShouldFailWithLeaveTypeDoesNotExist()
    {
        CreateLeaveRequestCommand command = new()
        {
            LeaveTypeId = 1
        };

        _fixture.repositoryMock
            .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(() => null);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.LeaveTypeId)
            .WithErrorMessage("Leave Type Id does not exist");
    }

    [Fact]
    public async Task TestValidatorShouldFailWhenStartDateIsGreaterThanEndDate()
    {
        CreateLeaveRequestCommand command = new()
        {
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now,
        };

        _fixture.repositoryMock
            .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new LeaveType());

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithoutErrorMessage("Start Date must be before End Date");
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithoutErrorMessage("End Date must be after Start Date");
    }

    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        CreateLeaveRequestCommand command = new()
        {
            LeaveTypeId = 1,
            RequestingEmployeeId = "123",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };

        _fixture.repositoryMock
            .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new LeaveType());

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion
}
