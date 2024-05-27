using Asp.Versioning.ApiExplorer;

namespace CleanArch.Api.Swagger;

public static class SwaggerApplicationRegistration
{
    /// <summary>
    /// Adds a Swagger UI middleware to the application builder.
    /// </summary>
    /// <param name="app">Builder for configuring the application's request pipeline</param>
    public static WebApplication UseSwaggerUI(this WebApplication app)
    {
        app.UseSwaggerUI(options =>
        {
            IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

            // build a swagger endpoint for each discovered API version
            foreach (var description in descriptions)
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                var name = description.GroupName.ToUpperInvariant();

                options.SwaggerEndpoint(url, name);
            }
        });

        return app;
    }
}
