using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Auth;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var tokenStr = await _authService.LoginAsync(dto);
            return Ok(new { token = tokenStr });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = await _authService.RegisterAsync(dto);
            return CreatedAtAction("Register", new { id = user.Id }, user);
        }
    }
}
