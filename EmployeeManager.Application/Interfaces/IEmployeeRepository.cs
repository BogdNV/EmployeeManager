using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;

namespace EmployeeManager.Application.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> SearchAsync(string search);
    }
}