using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Infra.Repository
{
    public class UserRepository(AppDbContext db) : IUserRepository
    {

        public async Task AddAsync(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
        }
        public Task<User?> GetByEmailAsync(string email)
        {
            return db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        /*
         * 1- Find the user
         * var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
         * 2- Check Pass
         * if(user is null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
         *     return Unauthorized();
         *     3-Generate Token
         * var token = // Create token
         * return Ok(token);
        */

        public async Task<User> CreateAsync(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return user;
        }


        public async Task<User> DeleteAsync(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) throw new InvalidOperationException("User not found");
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(int id, User updatedUser)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) throw new InvalidOperationException("User not found");
            // Update user properties
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return user;
        }
    }
}
