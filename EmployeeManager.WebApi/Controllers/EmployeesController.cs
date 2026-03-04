using EmployeeManager.Application.DTOs;
using EmployeeManager.Application.DTOs.Request;
using EmployeeManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            var employee = await _service.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeRequest request)
        {
            var employeeDto = new EmployeeDto
            {
                FirstName = request.FirstName,
                Surname = request.Surname,
                Patronymic = request.Patronymic,
                Address = request.Address,
                Department = request.Department,
                DateOfBirth = request.DateOfBirth,
                AboutMe = request.AboutMe
            };

            var id = await _service.CreateEmployeeAsync(employeeDto);

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEmployeeRequest request)
        {
            var existing = await _service.GetEmployeeByIdAsync(id);
            if (existing == null)
                return NotFound();
            existing.Surname = request.Surname;
            existing.Department = request.Department;
            existing.Address = request.Address;
            existing.AboutMe = request.AboutMe;
            await _service.UpdateEmployeeAsync(id, existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var employees = await _service.SearchAsync(query);
            return Ok(employees);
        }
    }
}