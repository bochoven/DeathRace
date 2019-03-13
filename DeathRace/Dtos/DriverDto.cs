using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeathRace.Models
{
    public class DriverDto
    {
        public int DriverId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string GivenName { get; set; }
        public string Preposition { get; set; }
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DOB { get; set; }

        public ICollection<Car> Cars { get; }

    }
}
