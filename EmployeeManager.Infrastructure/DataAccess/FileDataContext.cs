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
        public List<Employee> Employees { get; } = new();

        public FileDataContext(string path = "employees.json")
        {
            _path = path;
            LoadFromFile();
        }

        public int GetNextId()
        {
            return Employees.Max(x => x.Id) + 1;
        }

        public void Reset()
        {
            Employees.Clear();
        }

        void LoadFromFile()
        {
            if (File.Exists(_path))
            {
                var json = File.ReadAllText(_path);
                var employees = JsonSerializer.Deserialize<List<EmployeeDto>>(json);

                if (employees != null)
                {
                    Reset();
                    foreach (var item in employees)
                    {
                        Employees.Add(Employee.Create(
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
            var json = JsonSerializer.Serialize(Employees, options);
            File.WriteAllText(_path, json);
        }
    }
}