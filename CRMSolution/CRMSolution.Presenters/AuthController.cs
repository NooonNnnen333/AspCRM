using CRMSolution.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CRMSolution.Presenters;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public AuthController(IConfiguration configuration, ITokenService tokenService)
    {
        _configuration = configuration;
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto request)
    {
        var username = _configuration["Auth:Username"];
        var password = _configuration["Auth:Password"];

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Auth is not configured.");
        }

        var valid = string.Equals(request.Username, username, StringComparison.Ordinal)
            && string.Equals(request.Password, password, StringComparison.Ordinal);

        if (!valid)
        {
            return Unauthorized();
        }

        var token = _tokenService.CreateToken(request.Username);
        return Ok(new TokenResponseDto(token));
    }
}
