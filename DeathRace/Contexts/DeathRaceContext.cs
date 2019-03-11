using Microsoft.EntityFrameworkCore;
using DeathRace.Models;

namespace DeathRace.Contexts
{
    public class DeathRaceContext : DbContext
    {
        public DeathRaceContext(DbContextOptions<DeathRaceContext> options) 
            : base(options)
        {
        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Car> Cars { get; set; }

    }
}
