using Microsoft.EntityFrameworkCore;

namespace DeathRace.Models
{
    public class DeathRaceContext : DbContext
    {
        public DeathRaceContext(DbContextOptions<DeathRaceContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }

    }
}
