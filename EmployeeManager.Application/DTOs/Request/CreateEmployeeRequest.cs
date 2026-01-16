using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Application.DTOs.Request
{
    public class CreateEmployeeRequest
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AboutMe { get; set; }
        public string FullName => $"{Surname} {FullName} {Patronymic}";
    }
}