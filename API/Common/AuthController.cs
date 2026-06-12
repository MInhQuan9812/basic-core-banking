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
        //public async Task<IActionResult> Register(RegisterDto request)
        //{
        //    var result= await service.RegisterAsync(request);
        //}
    }
}
