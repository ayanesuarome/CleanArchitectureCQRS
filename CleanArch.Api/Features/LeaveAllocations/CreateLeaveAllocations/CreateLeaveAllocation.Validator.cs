using CleanArch.Application.Extensions;
using CleanArch.Domain.Errors;
using CleanArch.Domain.Repositories;
using FluentValidation;

namespace CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;

public static partial class CreateLeaveAllocation
{
    public sealed class Validator : AbstractValidator<Command>
    {
        private readonly ILeaveTypeRepository _repository;

        public Validator(ILeaveTypeRepository repository)
        {
            _repository = repository;

            RuleFor(m => m.LeaveTypeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                    .WithError(ValidationErrors.CreateLeaveAllocation.LeaveTypeIdIsRequired)
                .MustAsync(LeaveTypeMustExist)
                    .WithError(DomainErrors.LeaveAllocation.LeaveTypeMustExist);
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
        {
            return await _repository.AnyAsync(id, token);
        }
    }
}
