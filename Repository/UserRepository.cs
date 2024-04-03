using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using storeAPI.Data;
using storeAPI.Interfaces.Repository;
using storeAPI.Models;

namespace storeAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;
        public UserRepository(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllUsers(string role)
        {
            return await _userManager.GetUsersInRoleAsync(role);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<User?> CreateUser(User user)
        {
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if(result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Staff");
                if(roleResult.Succeeded)
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<User?> UpdateUser(int id, User user)
        {
            var userToUpdate = await _userManager.FindByIdAsync(id.ToString());
            if(userToUpdate == null)
            {
                return null;
            }
            userToUpdate.UserName = user.UserName;
            userToUpdate.FullName = user.FullName;
            userToUpdate.Email = user.Email;
            userToUpdate.Address = user.Address;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            userToUpdate.PasswordHash = _userManager.PasswordHasher.HashPassword(userToUpdate, user.PasswordHash);
            var result = await _userManager.UpdateAsync(userToUpdate);
            if(result.Succeeded)
            {
                return userToUpdate;
            }
            return null;
        }

        public async Task<User?> DeleteUser(int id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id.ToString());
            if(userToDelete == null)
            {
                return null;
            }
            var result = await _userManager.DeleteAsync(userToDelete);
            if(result.Succeeded)
            {
                return userToDelete;
            }
            return null;
        }
    }
}