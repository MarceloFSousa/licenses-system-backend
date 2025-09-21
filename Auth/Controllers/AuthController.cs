using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly JwtService _jwtService;
    private readonly AuthService _authService;

    public AuthController(UserService userService, JwtService jwtService,AuthService authService)
    {
        _userService = userService;
        _jwtService = jwtService;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.GetByEmailAsync(request.Email);

        
        if (user == null)
            return Unauthorized();
        var validatedUser = await _authService.AuthenticateAsync(user, request.Password);

        var token = _jwtService.GenerateToken(validatedUser);
        return Ok(new { Token = token, Role=user.Role });

    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var hashedPassword = _authService.HashesPassword(request.Password);
        var user = await _userService.CreateAsync(request,hashedPassword);

        if (user == null)
            return Unauthorized();

        var token = _jwtService.GenerateToken(user);
        return Ok(new { Token = token, Role=user.Role });
    }
}

