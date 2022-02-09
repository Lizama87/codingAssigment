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

namespace codingAssigment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrewsController : ControllerBase
    {
        private readonly DataContext _context;

        public CrewsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Crews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crew>>> GetCrews()
        {
            List<Crew> crews = await _context.Crews.Include(x => x.Employees).ToListAsync();
            string json = JsonConvert.SerializeObject(crews, Formatting.Indented);
            return Ok(json);
        }

        // GET: api/Crews/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Crew>> GetCrew(int id)
        {
            Crew crew = await _context.Crews.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);

            if (crew == null)
            {
                return NotFound();
            }

            string json = JsonConvert.SerializeObject(crew, Formatting.Indented);
            return Ok(json);
        }

        // PUT: api/Crews/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrew(int id, BodyCrew bodyCrew)
        {
            Crew crew = await _context.Crews.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);

            if (crew == null || id != bodyCrew.Id)
            {
                return BadRequest();
            }

            crew.Name = bodyCrew.Name;
            crew.Ranch = bodyCrew.Ranch;
            crew.Employees = new List<Employee>();
            crew.Employees = await _context.Employees.Where(e => bodyCrew.EmployeesIDs.Contains(e.Id)).ToListAsync();
            
            _context.Entry(crew).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Crews.Any(e => e.Id == id))
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

        // POST: api/Crews
        [HttpPost]
        public async Task<ActionResult<Crew>> PostCrew(BodyCrew bodyCrew)
        {
            Crew crew = new Crew();

            crew.Id = bodyCrew.Id;
            crew.Name = bodyCrew.Name;
            crew.Ranch = bodyCrew.Ranch;
            crew.Employees = await _context.Employees.Where(e => bodyCrew.EmployeesIDs.Contains(e.Id)).ToListAsync();


            _context.Crews.Add(crew);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrew", new { id = crew.Id }, crew);
        }

        // DELETE: api/Crews/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrew(int id)
        {
            var crew = await _context.Crews.FindAsync(id);
            if (crew == null)
            {
                return NotFound();
            }

            _context.Crews.Remove(crew);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
