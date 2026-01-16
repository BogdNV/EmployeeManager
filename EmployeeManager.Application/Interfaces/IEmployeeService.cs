using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Application.DTOs.Request;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(int id, Employee update);
        Task DeleteEmployeeAsync(int id);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    }
}