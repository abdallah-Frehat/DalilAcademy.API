using DalilAcademy.Core.DTOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Security;
using DalilAcademy.Core.Service;
using DalilAcademy.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using static DalilAcademy.Core.DTOs.Auth;

namespace DalilAcademy.Infra.Service
{
    public class AuthService(IUserRepository userRepository, IStudentRepository studentRepository, 
     IPasswordHasher passwordHasher, ITokenService tokenService) : IAuthService
    {
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await userRepository.GetByEmailAsync(dto.Email);

            if (user is null || !passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var (token, expiresAt) = tokenService.CreateToken(user);

            return new LoginResponseDto(token, expiresAt, user.Role);
        }
        public async Task<RegisterDto> RegisterAsync(RegisterDto registerDto)
        {
            //نتاكد  انه  ما  اتكرر الكود انه  مو  موجود  من  قبل    
            var existingUser = await userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            var passwordHash = passwordHasher.Hash(registerDto.password);

            var newUser = new User
            {
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Role = Roles.Student //نختار انه  يكون  طالب  عشان  امن  النظام  
            };

            await userRepository.AddAsync(newUser);

            var student = new Student
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserId = newUser.Id 
            };

            await studentRepository.AddAsync(student);

            var (token, expiresAt) = tokenService.CreateToken(newUser);

            return new RegisterDto(
                newUser.Email,
                registerDto.password, 
                student.FullName,
                student.PhoneNumber,
                newUser.Role
            );
        }
    }
}
