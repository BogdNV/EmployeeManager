using System.Reflection;
using EmployeeManager.Application.DTOs;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Infrastructure.Repositories
{
    // in-memory
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new();
        private int _nextId = 1;
        public async Task AddAsync(EmployeeDto entity)
        {
            var employee = Employee.Create(
                _nextId++,
                entity.FirstName,
                entity.Surname,
                entity.Patronymic,
                entity.Address,
                entity.Department,
                entity.DateOfBirth,
                entity.AboutMe
            );

            _employees.Add(employee);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = _employees.FirstOrDefault(x => x.Id == id);

            if (employee != null)
            {
                _employees.Remove(employee);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            return await Task.FromResult(_employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                Surname = e.Surname,
                Patronymic = e.Patronymic,
                DateOfBirth = e.DateOfBirth,
                Department = e.Department,
                Address = e.Address,
                AboutMe = e.AboutMe
            }));
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var e = _employees.FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                Surname = e.Surname,
                Patronymic = e.Patronymic,
                DateOfBirth = e.DateOfBirth,
                Department = e.Department,
                Address = e.Address,
                AboutMe = e.AboutMe
            });
        }

        public async Task<IEnumerable<EmployeeDto>> SearchAsync(string search)
        {
            return await Task.FromResult(_employees
                .Where(e => e.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            e.Surname.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            e.Department.Contains(search, StringComparison.OrdinalIgnoreCase))
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    Surname = e.Surname,
                    Patronymic = e.Patronymic,
                    DateOfBirth = e.DateOfBirth,
                    Department = e.Department,
                    Address = e.Address,
                    AboutMe = e.AboutMe
                })
            );
        }

        public async Task UpdateAsync(EmployeeDto entity)
        {
            var employee = Employee.Create(
                entity.Id,
                entity.FirstName,
                entity.Surname,
                entity.Patronymic,
                entity.Address,
                entity.Department,
                entity.DateOfBirth,
                entity.AboutMe
            );
            int index = _employees.IndexOf(employee);
            _employees[index] = employee;
            await Task.CompletedTask;
        }
    }
}