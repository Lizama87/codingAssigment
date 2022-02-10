#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using codingAssigment;
using codingAssigment.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace codingAssigment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            List<Employee> employees = await _context.Employees.ToListAsync();
            string json = JsonConvert.SerializeObject(employees, Formatting.Indented);
            return Ok(json);
        }

        // GET: api/Employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            Employee employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            string json = JsonConvert.SerializeObject(employee, Formatting.Indented);
            return Ok(json);
        }

        // PUT: api/Employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.Id == id))
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

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(JToken employeeJson)
        {
            if (employeeJson is JArray)
			{
                List<Employee> employees = null;

                try
				{
                    employees = JsonConvert.DeserializeObject<List<Employee>>(employeeJson.ToString());
				}
                catch (JsonSerializationException ex)
				{
                    return BadRequest("Error in Json Format");
                }
                
                if (employees == null)
                    return BadRequest();

                _context.Employees.AddRange(employees);
            }
            else if (employeeJson is JObject)
			{
                Employee employee = null;
                try
                {
                    employee = JsonConvert.DeserializeObject<Employee>(employeeJson.ToString());
                }
                catch (JsonSerializationException ex)
                {
                    return BadRequest("Error in Json Format");
                }

                if (employee == null)
                    return BadRequest();

                _context.Employees.Add(employee);
            }
            else
                return BadRequest();

            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/Employees/{id}
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
    }
}
