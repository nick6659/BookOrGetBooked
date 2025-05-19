using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
    {
        var result = await _authService.RegisterAsync(model);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var result = await _authService.LoginAsync(model);
        return result.IsSuccess ? Ok(result) : Unauthorized(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto model)
    {
        var result = await _authService.RefreshTokenAsync(model);
        return result.IsSuccess ? Ok(result) : Unauthorized(result);
    }

}
