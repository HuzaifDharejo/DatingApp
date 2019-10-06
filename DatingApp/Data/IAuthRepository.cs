using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
   public  interface IAuthRepository
    {
        Task<User> Rigster(User user , string password);
        Task<User> LogIn(string username, string pasword );
        Task<bool> UserExist(string username );
    }
}
