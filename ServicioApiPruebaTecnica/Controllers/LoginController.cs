using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json", "application/xml")]

public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public ActionResult<string> Login([FromBody] LoginRequest model)
    {
        var user = _userService.ValidateUser(model.Username, model.Password);
        if (user == null)
            return Unauthorized();

        var token = _tokenService.GenerateToken(user);
        return Ok(new { Token = token });
    }
}
