using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Application.DTOs.Request
{
    public class UpdateEmployeeRequest
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string AboutMe { get; set; }

    }
}