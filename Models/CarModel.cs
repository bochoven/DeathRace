using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeathRace.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        [Range(1900, 3000)]
        public int Year { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
