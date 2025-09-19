using Application.Contracts.Infrastructure;
using Application.Models;

using Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
       public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings")); 
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
