using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Infrastructure.DataAccess;
using EmployeeManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDataContext, InMemoryDataContext>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDataContext, FileDataContext>();

            return services;
        }
    }
}