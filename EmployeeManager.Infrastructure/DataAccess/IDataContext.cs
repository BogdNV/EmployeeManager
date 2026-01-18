using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Infrastructure.DataAccess
{
    public interface IDataContext
    {
        List<Employee> Employees { get; }
        int GetNextId();
        void Reset();
    }
}