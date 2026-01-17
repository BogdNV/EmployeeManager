using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Text.Unicode;
using EmployeeManager.Application.DTOs;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Infrastructure.FileRepository
{
    public class FileEmployeeRepository : IEmployeeRepository
    {
        readonly string _filePath;
        List<Employee> _employees;
        public FileEmployeeRepository(string filePath = "employees.json")
        {
            _filePath = filePath;
            LoadFromFile();
        }

        void LoadFromFile()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _employees = JsonSerializer.Deserialize<List<Employee>>(json) ??
                    new();
            }
            else
            {
                _employees = new();
            }
        }

        void SaveToFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };
            var json = JsonSerializer.Serialize(_employees, options);
            File.WriteAllText(_filePath, json);
        }

        public Task AddAsync(EmployeeDto entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeDto>> SearchAsync(string search)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EmployeeDto entity)
        {
            throw new NotImplementedException();
        }
    }
}