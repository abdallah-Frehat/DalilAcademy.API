using DalilAcademy.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Security
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool VerifyPassword(string password, string hash);
        //string HashPassword(string password);
        //bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
