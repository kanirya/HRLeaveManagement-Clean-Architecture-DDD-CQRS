using Application.Common.Behaviours;
using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence.Auth;
using Application.Profiles;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            // Register all FluentValidators automatically
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register MediatR Pipeline Behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            return services;
        }
    }
}
