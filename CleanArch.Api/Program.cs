using CleanArch.Api.Middlewares;
using CleanArch.Application;
using CleanArch.Infrastructure;
using CleanArch.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddCleanArchEFDbContext(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices();

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
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Custom middlewares
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
