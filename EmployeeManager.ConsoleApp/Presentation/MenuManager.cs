using EmployeeManager.Application.DTOs;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.ConsoleApp.Presentation.Interfaces;

namespace EmployeeManager.ConsoleApp.Presentation
{
    public interface IMenuManager
    {
        Task RunAsync();
    }

    public class MenuManager : IMenuManager
    {
        readonly IEmployeeService _service;
        readonly IConsoleUI _ui;

        public MenuManager(IEmployeeService service, IConsoleUI consoleUI)
        {
            _ui = consoleUI;
            _service = service;

        }
        public async Task RunAsync()
        {
            bool exit = false;

            while (!exit)
            {
                _ui.ClearScreen();
                _ui.DisplayHeader("Управление сотрудниками");

                Console.WriteLine("1. Добавить сотрудника");
                Console.WriteLine("2. Просмотр всех сотрудников");
                Console.WriteLine("3. Просмотр деталей сотрудника");
                Console.WriteLine("4. Изменить данные о сотруднике");
                Console.WriteLine("5. Удалить сотрудника");
                Console.WriteLine("6. Найти сотрудника");
                Console.WriteLine("0. Выход");

                var input = _ui.ReadString("Выберите действие");

                switch (input)
                {
                    case "1": await AddEmployee(); break;
                    case "2": await ShowAllEmployees(); break;
                    case "3": await ShowEmployeeDetails(); break;
                    case "4": await UpdateEmployee(); break;
                    case "5": await DeleteEmployee(); break;
                    case "6": await FindEmployees(); break;
                    case "0": exit = true; break;
                    default: _ui.DisplayError("Неверный набор"); break;
                }
            }
        }

        async Task AddEmployee()
        {
            _ui.ClearScreen();
            _ui.DisplayHeader("Добавление нового сотрудника");

            var request = new EmployeeDto
            {
                FirstName = _ui.ReadString("Введите имя"),
                Surname = _ui.ReadString("Введите фамилию"),
                Patronymic = _ui.ReadString("Введите отчество"),
                DateOfBirth = _ui.ReadDate("Введите дату рождения"),
                Address = _ui.ReadString("Введите адрес проживания", false),
                Department = _ui.ReadString("Введите отдел"),
                AboutMe = _ui.ReadString("Информация о себе", false)
            };

            try
            {
                await _service.CreateEmployeeAsync(request);
                _ui.DisplaySuccess("Сотрудник успешно добавлен!");
            }
            catch (Exception ex)
            {
                _ui.DisplayError($"Ошибка при добавлении: {ex.Message}");
            }
            finally
            {
                _ui.WaitForAnyKey();
            }


        }

        async Task UpdateEmployee()
        {
            _ui.ClearScreen();
            _ui.DisplayHeader("Редактирование сотрудника");


            var id = _ui.ReadInt("Введите ID сотрудника", 1);
            var employee = await _service.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                _ui.DisplayError("Сотрудник не найден!");
                _ui.WaitForAnyKey();
                return;
            }

            // DisplayEmployeeDetails(employee);
            _ui.DisplayMessage("\nВведите новые данные (оставте пустым чтобы не изменять):");

            var aboutMe = _ui.ReadString("Введите \"Информация о себе\"", false);
            if (!string.IsNullOrWhiteSpace(aboutMe))
                employee.AboutMe = aboutMe;

            var department = _ui.ReadString("Введите отдел", false);
            if (!string.IsNullOrWhiteSpace(department))
                employee.Department = department;

            var address = _ui.ReadString("Введите адрес проживания", false);
            if (!string.IsNullOrWhiteSpace(address))
                employee.Address = address;

            var surname = _ui.ReadString("Введите фамилию", false);
            if (!string.IsNullOrWhiteSpace(surname))
                employee.Surname = surname;

            try
            {
                await _service.UpdateEmployeeAsync(id, employee);
                _ui.DisplaySuccess("Данные успешно обновлены!");
            }
            catch (Exception ex)
            {
                _ui.DisplayError($"Ошибка: {ex.Message}");
            }
            finally
            {
                _ui.WaitForAnyKey();
            }
        }

        async Task ShowAllEmployees()
        {
            _ui.ClearScreen();
            _ui.DisplayHeader("Список всех сотрудников");

            var employees = await _service.GetAllEmployeesAsync();

            var columns = new Dictionary<string, Func<EmployeeDto, object>>
            {
                ["ID"] = e => e.Id,
                ["Имя"] = e => e.FirstName,
                ["Фамилия"] = e => e.Surname,
                ["Дата рождения"] = e => e.DateOfBirth.ToString("d"),
                ["Отдел"] = e => e.Department,
            };

            _ui.DisplayTable(employees, columns);
            _ui.WaitForAnyKey();

        }

        async Task ShowEmployeeDetails()
        {
            _ui.ClearScreen();
            _ui.DisplayHeader("Информация о сотруднике");

            var id = _ui.ReadInt("Введите ID сотрудника", 1);
            var employee = await _service.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                _ui.DisplayError("Сотрудник не найден!");

            }
            else
            {
                DisplayEmployeeDetails(employee);
            }
            _ui.WaitForAnyKey();
        }

        void DisplayEmployeeDetails<T>(T e) where T : EmployeeDto
        {
            Console.WriteLine($"ID: {e.Id}");
            Console.WriteLine($"Имя: {e.FirstName}");
            Console.WriteLine($"Фамилия: {e.Surname}");
            Console.WriteLine($"Отчество: {e.Patronymic}");
            Console.WriteLine($"Дата рождения: {e.DateOfBirth:d}");
            Console.WriteLine($"Адрес проживания: {e.Address ?? "не указан"}");
            Console.WriteLine($"Отдел: {e.Department}");
            Console.WriteLine($"Информация \"О себе\": {e.AboutMe ?? "не указан"}");
        }

        async Task DeleteEmployee()
        {
            _ui.ClearScreen();
            _ui.DisplayHeader("Удаление сотрудника");

            var id = _ui.ReadInt("Ведите ID сотрудника", 1);
            try
            {
                await _service.DeleteEmployeeAsync(id);
                _ui.DisplaySuccess("Сотрудник успешно удален");
            }
            catch (Exception ex)
            {
                _ui.DisplayError($"Ошибка при удалении: {ex.Message}");
            }
            finally
            {
                _ui.WaitForAnyKey();
            }

        }

        async Task FindEmployees()
        {
            _ui.ClearScreen();
            _ui.DisplayHeader("Поиск сотрудников");

            _ui.DisplayMessage("Критерии поиска");
            var search = _ui.ReadString("Имя, фамилия или отдел (частично)", false);

            var employees = await _service.SearchAsync(search);

            if (employees != null)
            {
                var columns = new Dictionary<string, Func<EmployeeDto, object>>
                {
                    ["ID"] = e => e.Id,
                    ["Имя"] = e => e.FirstName,
                    ["Фамилия"] = e => e.Surname,
                    ["Дата рождения"] = e => e.DateOfBirth.ToString("d"),
                    ["Отдел"] = e => e.Department,
                };
                _ui.DisplayTable(employees, columns);
            }
            else
            {
                _ui.DisplayMessage("Сотрудники не найдены");
            }
            _ui.WaitForAnyKey();
        }
    }
}