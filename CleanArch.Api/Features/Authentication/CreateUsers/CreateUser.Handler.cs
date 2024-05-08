using CleanArch.Application.Abstractions.Identity;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Api.Features.Authentication.CreateUsers;

public static partial class CreateUser
{
    public sealed class Handler : ICommandHandler<Command, Result<RegistrationResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IValidator<Command> _validator;

        public Handler(
        UserManager<User> userManager,
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

            Result<UserName> firstNameResult = UserName.Create(command.FirstName);
            Result<UserName> lastNameResult = UserName.Create(command.LastName);
            Result<Email> emailResult = Email.Create(command.Email);

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(firstNameResult, lastNameResult, emailResult);

            if (firstFailureOrSuccess.IsFailure)
            {
                return new FailureResult<RegistrationResponse>(firstFailureOrSuccess.Error);
            }

            User user = new(firstNameResult.Value, lastNameResult.Value)
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
