using CleanArch.Api.Features.LeaveRequests;
using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;
using FluentValidation.TestHelper;

namespace CleanArch.Application.Tests.Features.LeaveRequests.Shared;

public class BaseLeaveRequestCommandValidatorTest
{
    private readonly BaseCommandValidator _validator;

    public BaseLeaveRequestCommandValidatorTest()
    {
        _validator = new BaseCommandValidator();
    }

    //[Fact]
    //public async Task TestValidatorShouldFailWhenStartDateIsGreaterThanEndDate()
    //{
    //    UpdateLeaveRequest.Command command = new()
    //    {
    //        Id = 1,
    //        StartDate = DateTime.Now.AddDays(1),
    //        EndDate = DateTime.Now,
    //    };

    //    var result = await _validator.TestValidateAsync(command);

    //    result.ShouldHaveValidationErrorFor(x => x.StartDate)
    //        .WithoutErrorMessage("Start Date must be before End Date");
    //    result.ShouldHaveValidationErrorFor(x => x.EndDate)
    //        .WithoutErrorMessage("End Date must be after Start Date");
    //}

    [Fact]
    public async Task TestValidatorShouldNotFail()
    {
        CreateLeaveRequest.Command command = new(1, null)
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
