using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using regisztracio_be.Context;
using regisztracio_be.Dto;
using regisztracio_be.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace regisztracio_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {

        private readonly OTDbContext _context;
        public VehicleController(OTDbContext context)
        {
            _context = context;
        }

        // GET: api/<VehicleController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicles = await _context.Vehicles.ToListAsync();
            return Ok(vehicles);
        }

        // GET api/<VehicleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        // POST api/<VehicleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VehiclePostDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = new Vehicle
            {
                Name = value.Name,
                Location = value.Location,
                BuildYear = value.BuildYear,
                Owner = value.Owner
            };

            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = vehicle.Id }, vehicle);
        }

        // PUT api/<VehicleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VehicleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var v = await _context.Vehicles.FindAsync(id);
            if (v == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(v);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
