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
        private readonly ICarRepository _repo;
        private readonly IDriverRepository _driverRepo;

        public CarController(ICarRepository CarRepo, IDriverRepository DriverRepo)
        {
            _repo = CarRepo;
            _driverRepo = DriverRepo;
        }

        // GET api/car
        [Route("~/api/cars")]
        [HttpGet]
        public async Task<ActionResult<IQueryable<CarDto>>> GetCars(int? startyear)
        {
            var cars = await _repo.GetAllCars(startyear);
            return Ok(cars);
        }

        // GET api/car/5
        [HttpGet("{id}", Name = "GetCar")]
        public async Task<ActionResult<CarDto>> GetById(int id)
        {
            CarDto car = await _repo.GetById(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        // POST api/car
        [HttpPost]
        public async Task<ActionResult<CarDto>> PostCar(CarDto car)
        {
            if (car == null)
            {
                return BadRequest();
            }
            
            // Check for valid DriverID
            var driver = await _driverRepo.GetById(car.DriverId);
            if (driver == null)
            {
                ModelState.AddModelError("DriverID Error", "DriverID is invalid or missing");
                return BadRequest(ModelState);
            }

            await _repo.Add(car);
            return CreatedAtAction("GetById", new { id = car.CarId }, car);
        }

        // PUT api/car/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarDto car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }
            
            // Check for valid DriverID
            var driver = await _driverRepo.GetById(car.DriverId);
            if (driver == null)
            {
                ModelState.AddModelError("DriverID Error", "DriverID is invalid or missing");
                return BadRequest(ModelState);
            }

            var carObj = await _repo.GetById(id);
            if (carObj == null)
            {
                return NotFound();
            }
            else
            {
                await _repo.UpdateById(id, car);
            }
            return NoContent();
        }

        // DELETE api/car/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var carObj = await _repo.GetById(id);
            if (carObj == null)
            {
                return NotFound();
            }
            else
            {
                await _repo.Remove(id);
            }

            return NoContent();
        }
    }
}
