using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using backend.services;
using backend.services.authservice;
using Backend.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.AuthController;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase{
    private readonly AuthService _authService;
    private readonly JwtService _tokenService;
    public AuthController(AuthService authService,JwtService tokenService){_authService = authService;_tokenService = tokenService;}

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDTO request){
        //valida
        var username = request.Username.Trim();
        if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(request.Password)){ return BadRequest(new{message = "Falta usuário e senha"}); }
        if(username.Length < 3 || username.Length > 30){ return BadRequest(new{message = "Usuário deve ter entre 3 e 30 caracteres"}); }
        if(!IsValidPassword(request.Password)){ return BadRequest(new{message = "Senha deve ser de 8 a 40 caracteres"}); }
        var usernameExists = _authService.GetUserByUsername(username); if(usernameExists != null){ return BadRequest(new{message = "Usuário já cadastrado"}); }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = _authService.CreateUser(username,passwordHash);

        return StatusCode(201, new{message = "Usuário criado com sucesso",user = new{user.Id,user.Username,user.Role}});
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO request){
        var user = _authService.GetUserByUsername(request.Username);
        if (user == null ||!BCrypt.Net.BCrypt.Verify(request.Password,user.PasswordHash)){ return Unauthorized(new{message = "Usuário ou senha incorretos"}); }
        var token = _tokenService.GenerateToken(user);
        return Ok(new{token,user = new{user.Id,user.Username,user.Role}});
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me(){
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        var username =User.FindFirstValue(JwtRegisteredClaimNames.UniqueName) ?? User.FindFirstValue(ClaimTypes.Name);
        var role = User.FindFirstValue(ClaimTypes.Role);

        return Ok(new{id = userId,username,role});
    }

    private static bool IsValidPassword(string password){
        return password.Length >= 8 &&password.Length <= 40;
    }
}