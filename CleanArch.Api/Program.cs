using CleanArch.Api.Middlewares;
using CleanArch.Application;
using CleanArch.Infrastructure;
using CleanArch.Persistence;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddCleanArchEFDbContext(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices();
// The factory-activated middleware is added to the built-in container
builder.Services.AddTransient<ExceptionMiddleware>();

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
        //policy => policy.AllowAnyOrigin().AllowAnyMethod());
        policy => policy
            .WithOrigins("https://localhost:7162", "http://localhost:5195")
            .WithMethods("GET", "POST", "PUT", "DELETE"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware activated by convention and it is registered in the request processing pipeline
//app.UseMiddleware<ConventionalExceptionMiddleware>();

// Middleware activated by MiddlewareFactory and it is registered in the request processing pipeline.
// The factory-activated middleware is added to the built-in container with builder.Services.AddTransient<FactoryActivatedMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enables same CORS policy to the whole web servie.
app.UseCors(policyName: "CleanArchAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
