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
            crew.Employees = await _context.Employees.Where(e => bodyCrew.Employees.Contains(e.Id)).ToListAsync();
            
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
        public async Task<ActionResult<Crew>> PostCrew(JToken crewJson)
        {
            if (crewJson is JArray)
            {
                List<BodyCrew> unformatCrews = null;

                try
                {
                    unformatCrews = JsonConvert.DeserializeObject<List<BodyCrew>>(crewJson.ToString());
                }
                catch (JsonSerializationException ex)
                {
                    return BadRequest("Error in Json Format");
                }

                if (unformatCrews == null)
                    return BadRequest();

                List<Crew> crews = new List<Crew>();

                foreach (BodyCrew unformatCrew in unformatCrews)
                {
                    Crew crew = new Crew();

                    crew.Id = unformatCrew.Id;
                    crew.Name = unformatCrew.Name;
                    crew.Ranch = unformatCrew.Ranch;
                    crew.Employees = await _context.Employees.Where(e => unformatCrew.Employees.Contains(e.Id)).ToListAsync();

                    crews.Add(crew);
                }
                _context.Crews.AddRange(crews);
            }
            else if (crewJson is JObject)
            {
                BodyCrew unformatCrew = null;

                try
                {
                    unformatCrew = JsonConvert.DeserializeObject<BodyCrew>(crewJson.ToString());
                }
                catch (JsonSerializationException ex)
                {
                    return BadRequest("Error in Json Format");
                }

                if (unformatCrew == null)
                    return BadRequest();

                Crew crew = new Crew();

                crew.Id = unformatCrew.Id;
                crew.Name = unformatCrew.Name;
                crew.Ranch = unformatCrew.Ranch;
                crew.Employees = await _context.Employees.Where(e => unformatCrew.Employees.Contains(e.Id)).ToListAsync();

                _context.Crews.Add(crew);
            }
            else
                return BadRequest();

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Crews/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrew(int id)
        {
            Crew crew = await _context.Crews.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);

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
