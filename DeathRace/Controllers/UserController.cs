using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using DeathRace.Models;

namespace DeathRace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly DeathRaceContext _context;

        public DriverController(DeathRaceContext context)
        {
          _context = context;

          if (_context.Drivers.Count() == 0)
          {
              _context.Drivers.Add(new Driver {
                GivenName = "Matilda",
                Preposition = "the",
                LastName = "Hun",
                DOB = DateTime.Parse("1969-04-05")
              });
              _context.SaveChanges();

              _context.Cars.Add(new Car {
                Brand = "Mercedes",
                Model = "GLK",
                Type = "350",
                Year = 1998,
                DriverId = 1
              });
              _context.SaveChanges();

          }
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            return await _context.Drivers.Include(i => i.Cars).ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver(int id)
        {
            var Driver = await _context.Drivers.Include(i => i.Cars)
                .FirstOrDefaultAsync(i => i.DriverId == id);

            if (Driver == null)
            {
                return NotFound();
            }

            return Driver;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Driver>> PostDriver(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDriver), new { id = driver.DriverId }, driver);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriver(int id, Driver driver)
        {
            if (id != driver.DriverId)
            {
                return BadRequest();
            }

            _context.Entry(driver).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var Driver = await _context.Drivers.FindAsync(id);

            if (Driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(Driver);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
