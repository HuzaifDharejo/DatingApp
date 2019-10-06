using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength =4, ErrorMessage = "You must have to Use Pasword Between 4 and 8 Charectars")]
        public string Pasword { get; set; }

    }
}
