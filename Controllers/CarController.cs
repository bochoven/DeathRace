using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

          // if (_context.TodoItems.Count() == 0)
          // {
          //     // Create a new TodoItem if collection is empty,
          //     // which means you can't delete all TodoItems.
          //     _context.TodoItems.Add(new TodoItem { Name = "Item1" });
          //     _context.SaveChanges();
          // }
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
