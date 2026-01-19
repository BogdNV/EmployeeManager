using EmployeeManager.Application.DTOs;
using EmployeeManager.Application.Interfaces;

namespace EmployeeManager.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _repository = employeeRepository;
        }
        public async Task<int> CreateEmployeeAsync(EmployeeDto employee)
        {
            await _repository.AddAsync(employee);
            return employee.Id;
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<EmployeeDto>> SearchAsync(string search)
        {
            return await _repository.SearchAsync(search);
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDto employee)
        {
            await _repository.UpdateAsync(id, employee);
        }
    }
}