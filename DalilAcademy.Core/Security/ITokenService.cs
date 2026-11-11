using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Security
{
    public interface ITokenService
    {
        (string token, DateTime ExpiresAt) CreateToken(User user);
    }
}
