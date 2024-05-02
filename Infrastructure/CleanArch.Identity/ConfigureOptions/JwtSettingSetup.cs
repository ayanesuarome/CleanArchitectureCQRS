using CleanArch.Identity.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Identity.ConfigureOptions;

public sealed class JwtSettingSetup(IConfiguration configuration) : IConfigureOptions<JwtSettings>
{
    private const string SectionName = nameof(JwtSettings);
    private readonly IConfiguration _configuration = configuration;

    public void Configure(JwtSettings options)
    {
        _configuration
            .GetRequiredSection(SectionName)
            .Bind(options);
    }
}
