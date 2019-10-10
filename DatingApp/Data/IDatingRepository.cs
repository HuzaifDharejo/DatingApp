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
        public Task<bool> SaveAll();
        public Task<IAsyncEnumerable<Users>> GetUsers();
        public Task<Users> GetUser(int Id);
    }
}
