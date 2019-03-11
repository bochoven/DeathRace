using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IDriverRepository DriverRepo;

        public DriverController(DeathRaceContext context, IDriverRepository _repo)
        {
          DriverRepo = _repo;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            var drivers = await DriverRepo.GetAllDrivers();
            return Ok(drivers);
        }

        // GET api/values/5
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

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Driver>> PostDriver(Driver driver)
        {
            if (driver == null)
            {
                return BadRequest();
            }
            await DriverRepo.Add(driver);
            return CreatedAtRoute("GetDriver", new { id = driver.DriverId }, driver);

            // return CreatedAtRoute("GetContacts", new { Controller = "Contacts", id = item.MobilePhone }, item);
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriver(int id, Driver driver)
        {
            if (id != driver.DriverId)
            {
                return BadRequest();
            }

            // _context.Entry(driver).State = EntityState.Modified;
            // await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await DriverRepo.Remove(id);
            return NoContent();
        }
    }
}
