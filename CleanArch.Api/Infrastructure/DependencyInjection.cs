using CleanArch.Api.Features.LeaveRequests;
using CleanArch.Api.Features.Authentication.Login;
using CleanArch.Api.Features.Authentication.CreateUsers;
using CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;
using CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;
using CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;
using CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;
using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;
using FluentValidation;
using System.Reflection;
using CleanArch.Api.Behaviors;
using CleanArch.Infrastructure;

namespace CleanArch.Api.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds and configures AutoMapper and MediatR services to the service collection.
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<BaseController>(ServiceLifetime.Transient);
            //services.AddScoped<IValidator<BaseCommand>, BaseCommandValidator>();
            //services.AddScoped<IValidator<Login.Command>, Login.Validator>();
            //services.AddScoped<IValidator<CreateUser.Command>, CreateUser.Validator>();
            //services.AddScoped<IValidator<CreateLeaveType.Command>, CreateLeaveType.Validator>();
            //services.AddScoped<IValidator<UpdateLeaveType.Command>, UpdateLeaveType.Validator>();
            //services.AddScoped<IValidator<CreateLeaveAllocation.Command>, CreateLeaveAllocation.Validator>();
            //services.AddScoped<IValidator<ChangeLeaveRequestApproval.Command>, ChangeLeaveRequestApproval.Validator>();
            //services.AddScoped<IValidator<CreateLeaveRequest.Command>, CreateLeaveRequest.Validator>();
            //services.AddScoped<IValidator<UpdateLeaveRequest.Command>, UpdateLeaveRequest.Validator>();
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            });

            return services;
        }
    }
}
