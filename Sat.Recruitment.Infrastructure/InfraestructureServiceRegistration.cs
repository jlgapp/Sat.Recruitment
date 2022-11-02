using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Contracts;
using Sat.Recruitment.Infrastructure.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Infrastructure
{
    public static class InfraestructureServiceRegistration
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services)
        {
            services.AddScoped<IHandleUser, HandleUser>();
            return services;
        }
    }
}
