using EmployeeManager.Application.DTOs;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Infrastructure.DataAccess;

namespace EmployeeManager.Infrastructure.Repositories
{
    // in-memory
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(IDataContext context) : base(context) { }
        public async Task AddAsync(EmployeeDto entity)
        {
            var employee = Employee.Create(
                _context.GetNextId(),
                entity.FirstName,
                entity.Surname,
                entity.Patronymic,
                entity.Address,
                entity.Department,
                entity.DateOfBirth,
                entity.AboutMe
            );

            _context.Employees.Add(employee);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Id == id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            return await Task.FromResult(_context.Employees.Select(e => new EmployeeDto
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
            var e = _context.Employees.FirstOrDefault(x => x.Id == id);
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
            return await Task.FromResult(_context.Employees
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
            int index = _context.Employees.IndexOf(employee);
            _context.Employees[index] = employee;
            _context.SaveChanges();
            await Task.CompletedTask;
        }
    }
}