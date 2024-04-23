using CleanArch.Application.Abstractions.Identity;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Api.Features.Authentication.CreateUsers;

public static partial class CreateUser
{
    public sealed class Handler : IRequestHandler<Command, Result<RegistrationResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IValidator<Command> _validator;

        public Handler(
        UserManager<ApplicationUser> userManager,
        IJwtProvider jwtProvider,
        IValidator<Command> validator)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _validator = validator;
        }

        public async Task<Result<RegistrationResponse>> Handle(Command command, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return new FailureResult<RegistrationResponse>(ValidationErrors.CreateUser.CreateUserValidation(validationResult.ToString()));
            }

            Result<FirstName> firstNameResult = FirstName.Create(command.FirstName);
            Result<LastName> lastNameResult = LastName.Create(command.LastName);
            Result<Email> emailResult = Email.Create(command.Email);

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(firstNameResult, lastNameResult, emailResult);

            if (firstFailureOrSuccess.IsFailure)
            {
                return new FailureResult<RegistrationResponse>(firstFailureOrSuccess.Error);
            }

            ApplicationUser user = new(firstNameResult.Value, lastNameResult.Value)
            {
                Email = emailResult.Value,
                UserName = command.Email,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                return new FailureResult<RegistrationResponse>(ValidationErrors.CreateUser.CreateUserValidation(result.Errors.ToString()));
            }

            await _userManager.AddToRoleAsync(user, Roles.Employee);

            RegistrationResponse response = new(user.Id);

            return new SuccessResult<RegistrationResponse>(response);
        }
    }
}
