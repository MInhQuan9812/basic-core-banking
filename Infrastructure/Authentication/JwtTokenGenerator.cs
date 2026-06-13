using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Authentication
{

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        private static readonly JsonWebTokenHandler TokenHandler = new();
        private readonly SigningCredentials _signingCredentials;
        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            var secretKey = _jwtSettings.Secret
                ?? throw new InvalidOperationException("JWT Secret is not configured in application settings.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }

        public string GenerateToken(User user)
        {

            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret
                ?? throw new InvalidOperationException("JWT Secret is not configured."));

            var claims = new Dictionary<string, object>
            {
                { JwtRegisteredClaimNames.Sub, user.Id.ToString() },
                { JwtRegisteredClaimNames.Name, user.FullName },
                { JwtRegisteredClaimNames.Email, user.Email ?? string.Empty },
                { "role", user.userRole.ToString() },
                { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() }
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            return TokenHandler.CreateToken(tokenDescriptor);
        }

        public string GenerateRefreshToken()
        {

            byte[] randomNumber = RandomNumberGenerator.GetBytes(32);
            return WebEncoders.Base64UrlEncode(randomNumber);
        }
    }
}
