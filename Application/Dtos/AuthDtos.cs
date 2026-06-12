using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class AuthDtos
    {
        public record RegisterDto(string FullName, string Email, string PhoneNumber, string Password);
        public record LoginDto(string Email, string Password);
        public record LoginResponseDto(string Token, RefreshTokenDto RefreshToken, UserDto User);
        public record RefreshTokenDto(string Token, string RefreshToken, int userId, DateTime ExperiesAt, bool IsRevoked);
        public record UserDto(int Id, string FullName, string Email, UserRole userRole);
    }
}
