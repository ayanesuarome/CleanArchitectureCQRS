using CleanArch.Api.ExceptionHandlers;
using CleanArch.Api.Infrastructure;
using CleanArch.Api.Middlewares;
using CleanArch.Api.Swagger;
using CleanArch.Identity;
using CleanArch.Identity.Migrations;
using CleanArch.Infrastructure;
using CleanArch.Persistence;
using CleanArch.Persistence.Migrations;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Serilog
builder.Host.UseSerilog((context, config) => config
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));

builder.Services.AddCleanArchEFDbContext(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddPersistenceServices();
builder.Services.AddApiServices();

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

// The factory-activated middleware is added to the built-in container
//builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddTransient<RequestContextLoggingMiddleware>();

// registered with a singleton lifetime
// chaining Exception Handlers. They are called in the order they are registered
//builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
//builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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
            .WithOrigins("https://localhost:7162", "http://localhost:5195")
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
// app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestContextLoggingMiddleware>();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Adds Serilog
app.UseSerilogRequestLogging();

// Enables same CORS policy to the whole web servie.
app.UseCors(policyName: "CleanArchAll");

app.UseHttpsRedirection();

// Authentication and Authorization
app.UseAuthentication().UseAuthorization();

app.MapControllers();

// apply pending migrations
app.ApplyMigrations();
app.ApplyIdentityMigrations();

app.Run();
