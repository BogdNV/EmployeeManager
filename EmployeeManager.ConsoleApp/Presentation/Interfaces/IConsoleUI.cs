using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.ConsoleApp.Presentation.Interfaces
{
    public interface IConsoleUI
    {
        void DisplayMessage(string message);
        void DisplayError(string error);
        void DisplaySuccess(string message);
        void DisplayHeader(string header);
        void DisplayTable<T>(IEnumerable<T> items, Dictionary<string, Func<T, object>> columns);

        string ReadString(string prompt, bool required = true);
        int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue);
        DateTime ReadDate(string prompt);

        void ClearScreen();
        void WaitForAnyKey();
    }
}