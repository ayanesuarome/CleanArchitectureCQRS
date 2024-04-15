using CleanArch.Api.Features.LeaveRequests;
using CleanArch.Api.Features.LeaveTypes.CreateLeaveTypes;
using CleanArch.Api.Features.LeaveTypes.UpdateLeaveTypes;
using CleanArch.Api.Features.LeaveAllocations.CreateLeaveAllocations;
using CleanArch.Api.Features.LeaveAllocations.UpdateLeaveAllocations;
using CleanArch.Api.Features.LeaveRequests.ChangeLeaveRequestApprovals;
using CleanArch.Api.Features.LeaveRequests.CreateLeaveRequests;
using CleanArch.Api.Features.LeaveRequests.UpdateLeaveRequests;
using FluentValidation;

namespace CleanArch.Api.Infrastructure
{
    public static class ApiServiceRegistration
    {
        /// <summary>
        /// Adds and configures FluentValidation services to the service collection.
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<BaseCommandValidator>(ServiceLifetime.Transient);
            //services.AddScoped<IValidator<BaseCommand>, BaseCommandValidator>();
            //services.AddScoped<IValidator<CreateLeaveType.Command>, CreateLeaveType.Validator>();
            //services.AddScoped<IValidator<UpdateLeaveType.Command>, UpdateLeaveType.Validator>();
            //services.AddScoped<IValidator<CreateLeaveAllocation.Command>, CreateLeaveAllocation.Validator>();
            //services.AddScoped<IValidator<UpdateLeaveAllocation.Command>, UpdateLeaveAllocation.Validator>();
            //services.AddScoped<IValidator<ChangeLeaveRequestApproval.Command>, ChangeLeaveRequestApproval.Validator>();
            //services.AddScoped<IValidator<CreateLeaveRequest.Command>, CreateLeaveRequest.Validator>();
            //services.AddScoped<IValidator<UpdateLeaveRequest.Command>, UpdateLeaveRequest.Validator>();

            return services;
        }
    }
}
