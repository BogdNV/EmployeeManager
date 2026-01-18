using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using EmployeeManager.Application.DTOs;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Infrastructure.DataAccess
{
    public class FileDataContext : IDataContext
    {
        readonly string _path;
        readonly InMemoryDataContext _context;
        public List<Employee> Employees => _context.Employees;

        public FileDataContext(string path = "employees.json")
        {
            _path = path;
            _context = new();
            LoadFromFile();
        }

        public int GetNextId()
        {
            return _context.GetNextId();
        }

        public void Reset()
        {
            _context.Reset();
        }

        void LoadFromFile()
        {
            if (File.Exists(_path))
            {
                var json = File.ReadAllText(_path);
                var employees = JsonSerializer.Deserialize<List<EmployeeDto>>(json);

                if (employees != null)
                {
                    _context.Reset();
                    foreach (var item in employees)
                    {
                        _context.Employees.Add(Employee.Create(
                            item.Id,
                            item.FirstName,
                            item.Surname,
                            item.Patronymic,
                            item.Address,
                            item.Department,
                            item.DateOfBirth,
                            item.AboutMe
                        ));
                    }
                    int maxId = employees.Max(x => x.Id);
                    for (int i = 1; i <= maxId; i++)
                    {
                        GetNextId();
                    }
                }
            }
        }

        public void SaveChanges()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };
            var json = JsonSerializer.Serialize(_context.Employees, options);
            File.WriteAllText(_path, json);
        }
    }
}