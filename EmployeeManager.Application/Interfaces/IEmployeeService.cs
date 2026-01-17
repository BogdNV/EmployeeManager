using EmployeeManager.Application.DTOs;

namespace EmployeeManager.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployeeAsync(EmployeeDto employee);
        Task UpdateEmployeeAsync(int id, EmployeeDto update);
        Task DeleteEmployeeAsync(int id);
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<IEnumerable<EmployeeDto>> SearchAsync(string search);
    }
}