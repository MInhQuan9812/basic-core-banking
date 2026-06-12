using Application.Common;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Application.Dtos.AuthDtos;

namespace Application.Services
{
    public class AuthService(IJwtTokenGenerator jwt, IBankDbContext context, ILogger<AuthService> logger) : IAuthService
    {
        private string HashToken(string token)
        {
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
        public async Task<ServiceResult<AuthDtos.LoginResponseDto>> LoginAsync(AuthDtos.LoginDto request)
        {
            //var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == request.Email);
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return ServiceResult<LoginResponseDto>.Failure("Invalid email or password", 401);
            }
            var userDto = new UserDto(user.Id, user.FullName, user.Email, user.userRole);
            // 1. VẪN PHẢI SINH ACCESS TOKEN (JWT) ĐỂ CLIENT GỌI API
            var accessToken = jwt.GenerateToken(user);

            // 2. Xử lý hạ tầng Refresh Token bảo mật ngắn hạn
            var rawRefreshToken = jwt.GenerateRefreshToken();
            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                RefeshtokenValue = HashToken(rawRefreshToken),
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                IsRevoked = false
            };
            await context.RefreshTokens.AddAsync(refreshTokenEntity);
            await context.SaveChangesAsync();
            var refreshTokenDto = refreshTokenEntity.Adapt<RefreshTokenDto>();
            return ServiceResult<LoginResponseDto>.SuccessResult(new LoginResponseDto(accessToken, refreshTokenDto, userDto));

        }

        public Task<ServiceResult<AuthDtos.RefreshTokenDto>> RefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<AuthDtos.UserDto>> RegisterAsync(AuthDtos.RegisterDto request)
        {
            var user = new User
            {
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            await context.Users.AddAsync(user);
            var saved = await context.SaveChangesAsync();
            if (saved == 0)
            {
                return ServiceResult<UserDto>.Failure("Failed to register user.", 500);
            }
            logger.LogInformation("User registered successfully: {User}", user);
            return ServiceResult<UserDto>.SuccessResult(user.Adapt<UserDto>(), "User registered successfully.", 201);

        }
    }
}
