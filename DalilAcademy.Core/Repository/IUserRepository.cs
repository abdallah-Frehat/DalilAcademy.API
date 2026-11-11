using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User newuser); // تم توحيدها لتكون AddAsync فقط
        Task<User> UpdateAsync(int id, User updatedUser);
        Task<User> DeleteAsync(int id);
    }
}