namespace EmployeeManager.ConsoleApp
{
    public interface IEmployeeBuilder
    {
        IEmployeeBuilder Configuration();
        IEmployeeApplication Build();
    }
}