using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeathRace.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string GivenName { get; set; }
        public string Preposition { get; set; }
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DOB { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
