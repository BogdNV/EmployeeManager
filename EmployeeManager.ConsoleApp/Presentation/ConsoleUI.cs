using EmployeeManager.ConsoleApp.Presentation.Interfaces;

namespace EmployeeManager.ConsoleApp.Presentation
{
    public class ConsoleUI : IConsoleUI
    {
        public void ClearScreen()
        {
            Console.Clear();
        }

        public void DisplayError(string error)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка: {error}");
            Console.ForegroundColor = defaultColor;
        }

        public void DisplayHeader(string header)
        {
            Console.WriteLine(new string('=', header.Length + 4));
            Console.WriteLine($"  {header}  ");
            Console.WriteLine(new string('=', header.Length + 4));
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplaySuccess(string message)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{message}");
            Console.ForegroundColor = defaultColor;
        }

        public void DisplayTable<T>(IEnumerable<T> items, Dictionary<string, Func<T, object>> columns)
        {
            if (!items.Any())
            {
                DisplayMessage("Данные отсутствуют");
                return;
            }

            var columnsLength = new Dictionary<string, int>();
            foreach (var column in columns)
            {
                var minLength = column.Key.Length;
                foreach (var item in items)
                {
                    var value = column.Value(item)?.ToString()?.Trim() ?? "";
                    if (value.Length > minLength) minLength = value.Length;
                }
                columnsLength[column.Key] = minLength + 2;
            }

            foreach (var col in columnsLength)
            {
                Console.Write(col.Key.PadRight(col.Value));
            }
            Console.WriteLine();

            foreach (var col in columnsLength)
            {
                Console.Write(new string('-', col.Value));
            }
            Console.WriteLine();

            foreach (var item in items)
            {
                foreach (var col in columns)
                {
                    var message = col.Value(item)?.ToString()?.Trim() ?? "нет данных";
                    Console.Write(message.PadRight(columnsLength[col.Key]));
                }
                Console.WriteLine();
            }
        }

        public DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var input = Console.ReadLine()?.Trim();
                if (DateTime.TryParse(input, out DateTime date))
                    return date;
                DisplayError("Некорректный формат даты");
            }
        }

        public int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var input = Console.ReadLine()?.Trim();
                if (int.TryParse(input, out int result))
                {
                    if (result >= min && result <= max)
                        return result;
                    DisplayError($"Число должно быть в диапазоне от {min} до {max}");
                }
                else
                    DisplayError("Некорректный формат");
            }
        }

        public string ReadString(string prompt, bool required = true)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    if (required)
                    {
                        DisplayError("Поле обязательно для заполнения");
                        continue;
                    }
                    return null;
                }
                return input;
            }

        }

        public void WaitForAnyKey()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey(true);
        }
    }
}