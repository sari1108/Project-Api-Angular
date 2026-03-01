using ApiProject.DTO;
using ApiProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthorDto.RegisterDto dto)
        {
            var user = await _authService.Register(dto);

            if (user == null)
                return BadRequest(new { message = "המשתמש כבר קיים במערכת" });

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthorDto.LoginDto dto)
        {
            var user = await _authService.Login(dto);

            if (user == null)
                return Unauthorized(new { message = "אחד מהנתונים שהוקשו שגוי" });

            return Ok(user);
        }
    }
}