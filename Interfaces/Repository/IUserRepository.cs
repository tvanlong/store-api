using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storeAPI.Models;

namespace storeAPI.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers(string role);
        Task<User?> GetUserById(int id);
        Task<User?> CreateUser(User user);
        Task<User?> UpdateUser(int id, User user);
        Task<User?> DeleteUser(int id);
    }
}