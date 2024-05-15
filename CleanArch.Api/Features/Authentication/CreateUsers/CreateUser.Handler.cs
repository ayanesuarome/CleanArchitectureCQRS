using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Contracts.Identity;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Primitives.Result;
using CleanArch.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Api.Features.Authentication.CreateUsers;

public static partial class CreateUser
{
    internal sealed class Handler : ICommandHandler<Command, Result<RegistrationResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtProvider _jwtProvider;

        public Handler(
            UserManager<User> userManager,
            IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<RegistrationResponse>> Handle(Command command, CancellationToken cancellationToken)
        {
            Result<UserName> firstNameResult = UserName.Create(command.FirstName);
            Result<UserName> lastNameResult = UserName.Create(command.LastName);
            Result<Email> emailResult = Email.Create(command.Email);

            Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(firstNameResult, lastNameResult, emailResult);

            if (firstFailureOrSuccess.IsFailure)
            {
                return Result.Failure<RegistrationResponse>(firstFailureOrSuccess.Error);
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
                return Result.Failure<RegistrationResponse>(ValidationErrors.CreateUser.CreateUserValidation(result.Errors.ToString()));
            }

            await _userManager.AddToRoleAsync(user, Domain.Enumerations.Role.Employee.Name);

            RegistrationResponse response = new(user.Id);

            return Result.Success<RegistrationResponse>(response);
        }
    }
}
