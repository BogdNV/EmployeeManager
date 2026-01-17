using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Application.DTOs;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;

namespace EmployeeManager.Application.Interfaces
{
    public interface IEmployeeRepository : IRepository<EmployeeDto>
    {
        Task<IEnumerable<EmployeeDto>> SearchAsync(string search);
    }
}