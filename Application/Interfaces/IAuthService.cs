using Application.Common;
using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<AuthDtos.UserDto>> RegisterAsync(AuthDtos.RegisterDto request);
        Task<ServiceResult<AuthDtos.LoginResponseDto>> LoginAsync(AuthDtos.LoginDto request);
        Task<ServiceResult<AuthDtos.RefreshTokenDto>> RefreshToken(string refreshToken);
    }
}
