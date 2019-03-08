using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeathRace.Models
{
    public class Car
    {
        public int CarId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Type { get; set; }
        [Range(1900, 3000)]
        public int Year { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
