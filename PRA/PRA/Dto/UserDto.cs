using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRA.Dto
{
    public class UserDto
    {

        public int IDUserAcc { get; set; }
        [Required(ErrorMessage = "Email je obvezan")] //erro poruka ako ga nema
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Šifra je obvezna")] //erro poruka ako ga nema
        [Display(Name = "Šifra:")]
        public string Pass { get; set; }
        [Required(ErrorMessage = "Nadimak je obvezan")] //erro poruka ako ga nema
        [Display(Name = "Nadimak:")]
        public string Username { get; set; }
        public Nullable<int> IsActive { get; set; }
    }
}