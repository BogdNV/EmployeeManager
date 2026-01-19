using EmployeeManager.ConsoleApp.Presentation;
using EmployeeManager.ConsoleApp.Presentation.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManager.ConsoleApp
{
    public sealed class EmployeeBuilder : IEmployeeBuilder
    {
        private readonly IServiceCollection _services;
        public IServiceCollection Services => _services;

        public EmployeeBuilder()
        {
            _services = new ServiceCollection();
        }
        public IEmployeeApplication Build()
        {
            _services.AddSingleton<IMenuManager, MenuManager>();
            _services.AddScoped<IConsoleUI, ConsoleUI>();

            return new EmployeeApplication(_services.BuildServiceProvider());
        }

        public IEmployeeBuilder Configuration()
        {

            return this;
        }
    }
}