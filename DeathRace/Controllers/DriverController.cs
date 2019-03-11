using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeathRace.Models;
using DeathRace.Contexts;
using DeathRace.Repository;

namespace DeathRace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepository DriverRepo;

        public DriverController(DeathRaceContext context, IDriverRepository _repo)
        {
          DriverRepo = _repo;
        }

        // GET api/driver
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            var drivers = await DriverRepo.GetAllDrivers();
            return Ok(drivers);
        }

        // GET api/driver/5
        [HttpGet("{id}", Name = "GetDriver")]
        public async Task<IActionResult> GetById(int id)
        {
            var driver = await DriverRepo.GetById(id);
            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }

        // POST api/val   ues
        [HttpPost]
        public async Task<ActionResult<Driver>> PostDriver(Driver driver)
        {
            if (driver == null)
            {
                return BadRequest();
            }
            await DriverRepo.Add(driver);
            return CreatedAtRoute("GetDriver", new { id = driver.DriverId }, driver);
        }

        // PUT api/driver
        [HttpPut("{id}")]
        public async Task<ActionResult<Driver>> Update(int id, [FromBody] Driver driver)
        {
            if (id != driver.DriverId)
            {
                return BadRequest();
            }
            var driverObj = await DriverRepo.GetById(id);
            if (driverObj == null)
            {
                return NotFound();
            }
            else
            {
                await DriverRepo.UpdateById(id, driver);
            }
            return NoContent();
        }

        // DELETE api/driver/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await DriverRepo.Remove(id);
            return NoContent();
        }
    }
}
