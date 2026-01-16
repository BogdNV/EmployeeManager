using System.Reflection;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Infrastructure.Repositories
{
    // in-memory
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new();
        private int _nextId = 1;
        public async Task AddAsync(Employee entity)
        {
            PropertyInfo? property = typeof(Employee).GetProperty("Id");
            property?.SetValue(entity, _nextId++);

            _employees.Add(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await GetByIdAsync(id);

            if (employee != null)
            {
                _employees.Remove(employee);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await Task.FromResult(_employees);
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_employees.FirstOrDefault(x => x.Id == id));
        }

        public async Task<IEnumerable<Employee>> SearchAsync(string search)
        {
            return await Task.FromResult(_employees
                .Where(e => e.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            e.Surname.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            e.Department.Contains(search, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task UpdateAsync(Employee entity)
        {
            int index = _employees.IndexOf(entity);
            _employees[index] = entity;
            await Task.CompletedTask;
        }
    }
}