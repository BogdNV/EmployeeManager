using EmployeeManager.Application.Interfaces;
using EmployeeManager.Infrastructure.DataAccess;
using EmployeeManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFileInfrastructure(this IServiceCollection services, string path)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDataContext, FileDataContext>(p =>
            new FileDataContext(path));

            return services;
        }

        public static IServiceCollection AddInMemoryInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IDataContext, InMemoryDataContext>();
            return services;
        }
    }
}