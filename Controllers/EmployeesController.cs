using EmployeeManagement.Api.Data;
using EmployeeManagement.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Promtech_Machine_Test.Models;

namespace EmployeeManagement.Api.Controllers
{
    //ControllerBase is from the Asp.netcore Mvc Controller base gives automatic response handling provides
    //ok,not found without it we need to create it manually
    [Route("api/[controller]")]
    [ApiController]


    //Dependency Injection
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/employees/ Reads from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Department = e.Department,
                    Salary = e.Salary,
                    CreatedOn = e.CreatedOn
                })
                .OrderBy(e => e.Name)
                .ToListAsync();

            return Ok(employees);
        }

        // GET: api/employees/with id/convert to dto/ return 404 if it is not found / return 200 if it is found.
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Where(e => e.EmployeeId == id)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Email = e.Email,
                    Department = e.Department,
                    Salary = e.Salary,
                    CreatedOn = e.CreatedOn
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST: api/employees/ create the employees/validate input/check duplicate email/map to dto/ save to the database
        // /return 201 when the data is created.
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if email already exists
            var existingEmployee = await _context.Employees
                .AnyAsync(e => e.Email == createEmployeeDto.Email);

            if (existingEmployee)
            {
                return Conflict(new { message = "An employee with this email already exists." });
            }

            var employee = new Employee
            {
                Name = createEmployeeDto.Name,
                Email = createEmployeeDto.Email,
                Department = createEmployeeDto.Department,
                Salary = createEmployeeDto.Salary,
                CreatedOn = DateTime.UtcNow
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var employeeDto = new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                Salary = employee.Salary,
                CreatedOn = employee.CreatedOn
            };

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employeeDto);
        }

        // PUT: api/employees/5/Update the Employee/Validate the details/Fetch the details/
        //check duplicate email /update feilds and savechanges.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Check if email already exists for another employee
            var existingEmployee = await _context.Employees
                .AnyAsync(e => e.Email == updateEmployeeDto.Email && e.EmployeeId != id);

            if (existingEmployee)
            {
                return Conflict(new { message = "Another employee with this email already exists." });
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Department = updateEmployeeDto.Department;
            employee.Salary = updateEmployeeDto.Salary;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/employees/5 delete the employee/Find the employee/ remove from DB/Save changes/No content 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}