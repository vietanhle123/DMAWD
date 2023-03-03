using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practical_api_exam.Data;
using practical_api_exam.Models;

namespace practical_api_exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return BadRequest("Employee not found!");
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee request)
        {
            var employeeToBeUpdated = await _context.Employees.FindAsync(request.Id);
            if (employeeToBeUpdated == null)
            {
                return BadRequest("Employee not found");
            }

            employeeToBeUpdated.Name = request.Name;
            employeeToBeUpdated.Age = request.Age;
            employeeToBeUpdated.Position = request.Position;

            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employeeToBeDeleted = await _context.Employees.FindAsync(id);
            if (employeeToBeDeleted == null)
            {
                return BadRequest("Employee not found!");
            }
            _context.Employees.Remove(employeeToBeDeleted);
            await _context.SaveChangesAsync();
            return Ok($"Employee with Id: {id} has been deleted");
        }

    }
}