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
    public class UserController : ControllerBase
    {
        private readonly DeathRaceContext _context;

        public UserController(DeathRaceContext context)
        {
          _context = context;

          if (_context.Users.Count() == 0)
          {
              // Create a new TodoItem if collection is empty,
              // which means you can't delete all TodoItems.
              _context.Users.Add(new User { 
                GivenName = "Matilda",
                Preposition = "the",
                LastName = "Hun"
              });
              _context.SaveChanges();
              
              _context.Cars.Add(new Car { 
                Brand = "Mercedes",
                UserId = 1
              });
              _context.SaveChanges();

          }
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.Include(i => i.Cars).ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = await _context.Users.Include(i => i.Cars)
                .FirstOrDefaultAsync(i => i.UserId == id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { UserId = user.UserId }, user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
