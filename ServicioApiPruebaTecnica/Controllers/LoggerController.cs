using Microsoft.AspNetCore.Mvc;
using ServicioApiPruebaTecnica.Models.dataDTO;

namespace ServicioApiPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoggerController : Controller
    {

        //private readonly IAuthService _authService;

        //public LoggerController(IAuthService authService)
        //{
        //    _authService = authService;
        //}

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginDTO model)
        //{
        //    var token = _authService.Authenticate(model.Username, model.Password);

        //    if (token == null)
        //        return Unauthorized();

        //    return Ok(new { Token = token });
        //}
    }
}
