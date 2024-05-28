using Microsoft.Extensions.Options;

namespace CleanArch.Identity.Options;

internal sealed class JwtValidation : IValidateOptions<JwtOptions>
{
    public ValidateOptionsResult Validate(string? name, JwtOptions options)
    {
        if (options.DurationInMinutes < 60 || options.DurationInMinutes > 1440)
        {
            return ValidateOptionsResult.Fail($"{nameof(options.DurationInMinutes)} must be between 60 - 1440.");
        }

        return ValidateOptionsResult.Success;
    }
}
