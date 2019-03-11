using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using DeathRace.Models;
using DeathRace.Repository;

namespace DeathRace.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository CarRepo;
        private readonly IDriverRepository DriverRepo;

        public CarController(ICarRepository _repo, IDriverRepository _driverRepo)
        {
            CarRepo = _repo;
            DriverRepo = _driverRepo;
        }

        // GET api/car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars(int? startyear)
        {
            var cars = await CarRepo.GetAllCars(startyear);
            return Ok(cars);
        }

        // GET api/car/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await CarRepo.GetById(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        // POST api/car
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }
            
            // Check for valid DriverID
            var driver = await DriverRepo.GetById(car.DriverId);
            if (driver == null)
            {
                ModelState.AddModelError("DriverID Error", "DriverID is invalid or missing");
                return BadRequest(ModelState);
            }

            await CarRepo.Add(car);
            return CreatedAtAction("GetCar", new { id = car.CarId }, car);
        }

        // PUT api/car/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }
            
            // Check for valid DriverID
            var driver = await DriverRepo.GetById(id);
            if (driver == null)
            {
                ModelState.AddModelError("DriverID Error", "DriverID is invalid or missing");
                return BadRequest(ModelState);
            }

            var carObj = await CarRepo.GetById(id);
            if (carObj == null)
            {
                return NotFound();
            }
            else
            {
                await CarRepo.UpdateById(id, car);
            }
            return NoContent();
        }

        // DELETE api/car/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
          await CarRepo.Remove(id);
          return NoContent();
        }
    }
}
