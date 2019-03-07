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
    public class CarController : ControllerBase
    {
        private readonly DeathRaceContext _context;

        public CarController(DeathRaceContext context)
        {
          _context = context;

        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.Include(i => i.User).ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var Car = await _context.Cars.Include(i => i.User)
                .FirstOrDefaultAsync(i => i.CarId == id);

            if (Car == null)
            {
                return NotFound();
            }

            return Car;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car user)
        {
            _context.Cars.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCar), new { CarId = user.CarId }, user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car user)
        {
            if (id != user.CarId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var Car = await _context.Cars.FindAsync(id);

            if (Car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(Car);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
