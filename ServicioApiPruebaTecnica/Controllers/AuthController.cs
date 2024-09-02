using Microsoft.AspNetCore.Mvc;
using ServicioApiPruebaTecnica.Services;

namespace ServicioApiPruebaTecnica.Controllers
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var token = _authService.Authenticate(model.Username, model.Password);

            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}