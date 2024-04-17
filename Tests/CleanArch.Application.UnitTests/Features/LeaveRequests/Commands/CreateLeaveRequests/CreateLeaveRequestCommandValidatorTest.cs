using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Domain.Entities;
using FluentValidation.TestHelper;
using Moq;

namespace CleanArch.Application.Tests.Features.LeaveRequests.Commands.CreateLeaveRequests;

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
        CreateLeaveRequest.Command command = new(leaveTypeId, null);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.LeaveTypeId)
            .WithErrorMessage("The leave type ID is required.");
    }

    [Fact]
    public async Task TestValidatorShouldFailWithLeaveTypeDoesNotExist()
    {
        CreateLeaveRequest.Command command = new(1, null);

        _fixture.repositoryMock
            .Setup(m => m.GetByIdAsync(command.LeaveTypeId))
            .ReturnsAsync(() => null);
        _fixture.repositoryMock
            .Setup(m => m.AnyAsync(command.LeaveTypeId, default))
            .ReturnsAsync(false);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.LeaveTypeId)
            .WithErrorMessage("There must be an associated Leave type.");
    }

    [Fact]
    public async Task TestValidatorShouldFailWhenStartDateIsGreaterThanEndDate()
    {
        CreateLeaveRequest.Command command = new(1, null)
        {
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now,
        };

        _fixture.repositoryMock
            .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new LeaveType(It.IsAny<string>(), It.IsAny<int>()));

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.StartDate)
            .WithErrorMessage("The StartDate must be before EndDate.");
        result.ShouldHaveValidationErrorFor(x => x.EndDate)
            .WithErrorMessage("The EndDate must be after StartDate.");
    }

    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        CreateLeaveRequest.Command command = new(1, null)
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };

        _fixture.repositoryMock
            .Setup(m => m.GetByIdAsync(command.LeaveTypeId))
            .ReturnsAsync(new LeaveType(It.IsAny<string>(), It.IsAny<int>()));
        _fixture.repositoryMock
            .Setup(m => m.AnyAsync(command.LeaveTypeId, default))
            .ReturnsAsync(true);

        var result = await _fixture.validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion
}
