using EmployeeManager.ConsoleApp.Presentation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManager.ConsoleApp
{
    public class EmployeeApplication : IEmployeeApplication
    {
        readonly IServiceProvider _provider;

        public EmployeeApplication(IServiceProvider provider)
        {
            _provider = provider;
        }
        public void Run()
        {
            _provider.GetService<IMenuManager>()?.RunAsync();
        }
    }
}