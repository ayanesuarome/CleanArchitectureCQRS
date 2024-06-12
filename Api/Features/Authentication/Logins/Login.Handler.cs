using CleanArch.Application.Abstractions.Authentication;
using CleanArch.Application.Abstractions.Messaging;
using CleanArch.Domain.Authentication;
using CleanArch.Domain.Core.Primitives.Result;
using CleanArch.Domain.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Api.Features.Authentication.Logins;

public static partial class Login
{
    internal sealed class Handler : ICommandHandler<Command, Result<Response>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtProvider _jwtProvider;

        public Handler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IJwtProvider jwtProvider,
            IValidator<Command> validator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user is null)
            {
                return Result.Failure<Response>(DomainErrors.Authentication.InvalidEmailOrPassword);
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            
            if (!result.Succeeded)
            {
                return Result.Failure<Response>(DomainErrors.Authentication.InvalidEmailOrPassword);
            }

            string securityToken = await _jwtProvider.GenerateTokenAsync(user);
            Response response = new(securityToken);

            return Result.Success<Response>(response);
        }
    }
}
