using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Infrastructure.DataAccess
{
    public interface IDataContext
    {
        List<Employee> Employees { get; }
        int GetNextId();
        void Reset();
        void SaveChanges();
    }
}