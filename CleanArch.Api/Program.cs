using CleanArch.Api.Middlewares;
using CleanArch.Api.Swagger;
using CleanArch.Application;
using CleanArch.Identity;
using CleanArch.Infrastructure;
using CleanArch.Persistence;
using CleanArch.Persistence.Migrations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Serilog
builder.Host.UseSerilog((context, config) => config
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));

builder.Services.AddApplicationServices();
builder.Services.AddValidators();
builder.Services.AddCleanArchEFDbContext(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices();
builder.Services.AddIdentityServices(builder.Configuration);
// The factory-activated middleware is added to the built-in container
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddHttpLogging(options =>
{
    // Add the Origin header so it will not be redacted.
    options.RequestHeaders.Add("Origin");
    // Add the rate limiting headers so they will not be redacted. (DoS)
    options.RequestHeaders.Add("X-Client-Id");
    options.RequestHeaders.Add("X-Real-IP");
    options.ResponseHeaders.Add("Retry-After");
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CleanArchAll",
        policy => policy
            .WithOrigins("https://localhost:7162")
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .AllowAnyHeader());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

// Middleware activated by convention and it is registered in the request processing pipeline
//app.UseMiddleware<ConventionalExceptionMiddleware>();

// Middleware activated by MiddlewareFactory and it is registered in the request processing pipeline.
// The factory-activated middleware is added to the built-in container with builder.Services.AddTransient<FactoryActivatedMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Adds Serilog
app.UseSerilogRequestLogging();

// Enables same CORS policy to the whole web servie.
app.UseCors(policyName: "CleanArchAll");

// Authentication and Authorization
app.UseAuthentication().UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

// apply pending migrations
MigrationHelper.ApplyMigration(app.Services);

app.Run();
