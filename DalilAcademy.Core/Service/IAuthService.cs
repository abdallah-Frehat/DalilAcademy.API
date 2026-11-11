using DalilAcademy.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using static DalilAcademy.Core.DTOs.Auth;

namespace DalilAcademy.Core.Service
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
        Task <RegisterDto> RegisterAsync(RegisterDto registerDto);
    }
}
