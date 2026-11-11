using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DTOs
{
    public class Auth
    {
        public record LoginRequestDto(string Email, string Password);
        public record LoginResponseDto(string Token, DateTime ExpiresAt, string Role);
    }
}
