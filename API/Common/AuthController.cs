using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using static Application.Dtos.AuthDtos;

namespace API.Common
{
    public class AuthController(IAuthService service) : BaseController
    {
        [HttpPost("register")]
        [EnableRateLimiting("LoginLimmiter")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var result = await service.RegisterAsync(request);
            return result.Success ? StatusCode(201, result.Data) : BadRequest(result.Message);
        }

        [HttpPost("login")]
        [EnableRateLimiting("LoginLimmiter")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var result = await service.LoginAsync(request);
            return result.Success ? Ok(result.Data) : Unauthorized(result.Message);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await service.RefreshToken(refreshToken);
            return result.Success ? Ok(result.Data) : Unauthorized(result.Message);
        }
    }
}
