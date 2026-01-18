using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Infrastructure.DataAccess
{
    public class InMemoryDataContext : IDataContext
    {
        public List<Employee> Employees { get; } = new();
        int _nextId = 1;

        public int GetNextId()
        {
            return _nextId++;
        }

        public void Reset()
        {
            Employees.Clear();
            _nextId = 1;
        }
    }
}