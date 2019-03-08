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

        // GET api/car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.Include(i => i.Driver).ToListAsync();
        }

        // GET api/car/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var Car = await _context.Cars.Include(i => i.Driver)
                .FirstOrDefaultAsync(i => i.CarId == id);

            if (Car == null)
            {
                return NotFound();
            }

            return Car;
        }

        // POST api/car
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            var driver = _context.Drivers.SingleOrDefault(m => m.DriverId == car.DriverId);
            if (driver == null)
            {
                ModelState.AddModelError("DriverID Error", "DriverID is invalid or missing");
                return BadRequest(ModelState);
            }
 
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCar), new { id = car.CarId }, car);
        }

        // PUT api/car/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            // Niet DRY moet dit in een helper? Of een private method van de CarController?
            var driver = _context.Drivers.SingleOrDefault(m => m.DriverId == car.DriverId);
            if (driver == null)
            {
                ModelState.AddModelError("DriverID Error", "DriverID is invalid or missing");
                return BadRequest(ModelState);
            }

            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/car/5
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
