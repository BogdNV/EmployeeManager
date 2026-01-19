using EmployeeManager.Application;
using EmployeeManager.ConsoleApp;
using EmployeeManager.Infrastructure;

var builder = new EmployeeBuilder();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
var app = builder.Build();

app.Run();