using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
   public  interface IAuthRepository
    {
        Task<Users> Rigster(Users user , string password);
        Task<Users> LogIn(string username, string pasword );
        Task<bool> UserExist(string username );
    }
}
