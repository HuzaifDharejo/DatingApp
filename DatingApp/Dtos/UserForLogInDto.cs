using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Dtos
{
    public class UserForLogInDto
    {
       
        public string UserName { get; set; }
      

        public string Password { get; set; }

    }
}
