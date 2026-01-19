using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddScoped<IEmployeeService, EmployeeService>();
            return service;
        }
    }
}