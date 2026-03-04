using EmployeeManager.Application;
using EmployeeManager.Infrastructure;
using EmployeeManager.WebApi;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddWebApi(builder.Configuration);
        var app = builder.Build();

        app.MapControllers();
        app.Run();
    }
}