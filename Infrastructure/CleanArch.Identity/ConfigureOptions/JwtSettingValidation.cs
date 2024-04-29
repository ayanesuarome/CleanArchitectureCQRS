using CleanArch.Identity.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Identity.ConfigureOptions;

internal sealed class JwtSettingValidation : IValidateOptions<JwtSettings>
{
    private const string SectionName = nameof(JwtSettings);
    public readonly JwtSettings Configuration;

    public JwtSettingValidation(IConfiguration configuration)
    {
        Configuration = configuration
            .GetSection(SectionName)
            .Get<JwtSettings>();
    }

    public ValidateOptionsResult Validate(string? name, JwtSettings options)
    {
        if(options.DurationInMinutes < 60 || options.DurationInMinutes > 1440)
        {
            return ValidateOptionsResult.Fail($"{nameof(options.DurationInMinutes)} must be between 60 - 1440.");
        }

        return ValidateOptionsResult.Success;
    }
}
