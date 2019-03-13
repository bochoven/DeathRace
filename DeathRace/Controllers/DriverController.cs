using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeathRace.Models;
using DeathRace.Repository;

namespace DeathRace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepository _repo;

        public DriverController(IDriverRepository DriverRepo)
        {
          _repo = DriverRepo;
        }

        // GET api/driver
        [Route("~/api/drivers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetDrivers()
        {
            var drivers = await _repo.GetAllDrivers();
            return Ok(drivers);
        }

        // GET api/driver/5
        [HttpGet("{id}", Name = "GetDriver")]
        public async Task<ActionResult<Driver>> GetById(int id)
        {
            var driver = await _repo.GetById(id);
            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }

        // POST api/val   ues
        [HttpPost]
        public async Task<ActionResult<DriverDto>> PostDriver(DriverDto driver)
        {
            if (driver == null)
            {
                return BadRequest();
            }
            
            var driverObj = await _repo.GetById(driver.DriverId);
            if (driverObj != null)
            {
                ModelState.AddModelError("DriverID Error", "DriverID is already registered");
                return BadRequest(ModelState);
            }

            await _repo.Add(driver);
            return CreatedAtAction("GetById", new { id = driver.DriverId }, driver);
        }

        // PUT api/driver
        [HttpPut("{id}")]
        public async Task<ActionResult<DriverDto>> Update(int id, [FromBody] DriverDto driver)
        {
            if (id != driver.DriverId)
            {
                return BadRequest();
            }
            var driverObj = await _repo.GetById(id);
            if (driverObj == null)
            {
                return NotFound();
            }
            else
            {
                await _repo.UpdateById(id, driver);
            }
            return NoContent();
        }

        // DELETE api/driver/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var driverObj = await _repo.GetById(id);
            if (driverObj == null)
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
