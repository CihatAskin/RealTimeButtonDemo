using Microsoft.AspNetCore.Mvc;
using RealTimeButtonDemo.API.Services;
using RealTimeButtonDemo.Shared.DTOs;

namespace RealTimeButtonDemo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly IConfiguration _configuration;

    public AuthController(JwtService jwtService, IConfiguration configuration)
    {
        _jwtService = jwtService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var hardcodedPassword = _configuration["Authentication:HardcodedPassword"];
        var allowedUsers = _configuration.GetSection("Authentication:AllowedUsers").Get<string[]>();

        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new { Message = "Username and password are required" });
        }

        if (allowedUsers?.Contains(request.Username) == true && request.Password == hardcodedPassword)
        {
            var token = _jwtService.GenerateToken(request.Username);
            var expirationMinutes = _configuration.GetValue<int>("JwtSettings:ExpirationMinutes");
            
            return Ok(new LoginResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Username = request.Username
            });
        }

        return Unauthorized(new { Message = "Invalid credentials" });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(new { Message = "Logged out successfully" });
    }
}