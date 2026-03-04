using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Application;
using EmployeeManager.Infrastructure;

namespace EmployeeManager.WebApi
{
    public static class DIWebApi
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            var storageType = configuration["Storage:Type"] ?? "InMemory";

            switch (storageType)
            {
                case "File":
                    var filePath = configuration["Storage:FilePath"] ?? "employees.json";
                    services.AddFileInfrastructure(filePath);
                    break;
                default:
                    services.AddInMemoryInfrastructure();
                    break;
            }

            return services;
        }
    }
}