using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DalilAcademy.Core.DTOs.Auth;

namespace DalilAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult <LoginResponseDto>>Login (LoginRequestDto loginRequestDto)
        {
            return Ok(await authService.LoginAsync(loginRequestDto));
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterDto>> Register(RegisterDto registerDto)
        {
            try
            {
                var result = await authService.RegisterAsync(registerDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
