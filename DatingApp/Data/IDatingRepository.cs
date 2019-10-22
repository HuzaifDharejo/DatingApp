using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delet<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUser(int Id);
        Task<Photo> GetPhoto(int Id);
    }
}
