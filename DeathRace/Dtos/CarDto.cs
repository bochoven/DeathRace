using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeathRace.Models
{
    public class CarDto
    {
        public int CarId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Type { get; set; }
        [Range(1900, 2100)]
        public int Year { get; set; }
        [Required(ErrorMessage = "Required")]
        public int DriverId { get; set; }
        
        public CarDto(Car x)
        {
            CarId = x.CarId;
            Brand = x.Brand;
            Model = x.Model;
            Type = x.Type;
            Year = x.Year;
            DriverId = x.DriverId;
        }
        
    }
}
