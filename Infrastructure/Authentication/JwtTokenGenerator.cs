using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        // 1. Sử dụng strongly-typed options thay vì đọc IConfiguration thủ công
        // (Leverage strongly-typed options instead of manual IConfiguration string lookups)
        private readonly JwtSettings _jwtSettings;

        // 2. Tái sử dụng duy nhất một Handler cho toàn bộ vòng đời ứng dụng
        // (Cache and reuse a single Handler instance for the entire application lifecycle)
        private static readonly JwtSecurityTokenHandler TokenHandler = new();

        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(User user)
        {
            // 3. Sử dụng UTF8 để bảo vệ tính toàn vẹn của chuỗi Secret Key
            // (Always apply UTF8 to guarantee the byte integrity of the cryptographic Secret)
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret
                ?? throw new InvalidOperationException("JWT Secret is not configured."));

            // 4. Sử dụng JwtRegisteredClaimNames để giữ Token siêu nhỏ gọn, tiết kiệm băng thông
            // (Utilize JwtRegisteredClaimNames to keep the token payload ultra-compact and save bandwidth)
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.FullName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim("role", user.userRole.ToString()), // Dạng chuỗi gọn thay vì dùng XML URI
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Phòng chống Replay Attack
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30), // Fintech nên giữ token thời hạn ngắn (30-60 phút)
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = TokenHandler.CreateToken(tokenDescriptor);
            return TokenHandler.WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
