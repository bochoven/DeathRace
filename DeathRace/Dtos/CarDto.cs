using System.ComponentModel.DataAnnotations;

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

    }
}
