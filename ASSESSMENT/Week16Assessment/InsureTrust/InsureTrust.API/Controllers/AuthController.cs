using System.Security.Claims;
using InsureTrust.API.Common;
using InsureTrust.API.DTOs.Auth;
using InsureTrust.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureTrust.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _svc;
    public AuthController(IAuthService svc) => _svc = svc;

    /// <summary>Register a new customer account.</summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterDto dto)
    {
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        var user = await _svc.RegisterAsync(dto, uploadPath);
        return StatusCode(201, ApiResponse<UserDto>.Created(user, "Registration successful."));
    }

    /// <summary>Customer login – returns a JWT token.</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _svc.LoginAsync(dto);
        return Ok(ApiResponse<object>.Ok(new { token }, "Login successful."));
    }

    /// <summary>Admin login – returns a JWT token.</summary>
    [HttpPost("admin-login")]
    public async Task<IActionResult> AdminLogin([FromBody] LoginDto dto)
    {
        var token = await _svc.AdminLoginAsync(dto);
        return Ok(ApiResponse<object>.Ok(new { token }, "Admin login successful."));
    }

    /// <summary>Google OAuth2 login / registration.</summary>
    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto dto)
    {
        var token = await _svc.GoogleLoginAsync(dto);
        return Ok(ApiResponse<object>.Ok(new { token }, "Google login successful."));
    }

    /// <summary>Get the authenticated user's profile.</summary>
    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var user = await _svc.GetProfileAsync(GetUserId());
        return Ok(ApiResponse<UserDto>.Ok(user));
    }

    /// <summary>Admin: list all registered users.</summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _svc.GetAllUsersAsync();
        return Ok(ApiResponse<IEnumerable<UserDto>>.Ok(users));
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}
