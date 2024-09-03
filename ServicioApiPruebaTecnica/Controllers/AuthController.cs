using Microsoft.AspNetCore.Mvc;
using ServicioApiPruebaTecnica.Data;
using ServicioApiPruebaTecnica.MyLogging;
using ServicioApiPruebaTecnica.Services;

namespace ServicioApiPruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly PruebaTecnicaOMCContextDB _dbContext;
        private readonly ILogService _logService;
        private readonly IMyLogger _logger;

        public AuthController(IAuthService authService,IMyLogger logger, PruebaTecnicaOMCContextDB context, ILogService logService)
        {
            _authService = authService;
            _logger = logger;
            _dbContext = context;
            _logService = logService;
        }

        [HttpPost("login")]
        #region
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        #endregion
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var username = User.Identity?.Name ?? "Anonymous123";
            _logger.Log($"Request by {username}: POST /api/Auth/LoginRequest");
            _logger.Log("LoginRequest - Method started.");
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