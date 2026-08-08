using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using backend.Data;
using backend.Models;
using backend.services;
using Backend.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.AuthController;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase{
    private readonly AppDbContext _context;
    private readonly JwtService _tokenService;

    public AuthController(AppDbContext context, JwtService tokenService){
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDTO request){
        var username = request.Username.Trim(); //
        // valida entrada
        if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(request.Password)){
            return BadRequest("falta usuário e senha");
        }
        if(username.Length < 3 || username.Length > 30){
            return BadRequest(new{message = "user deve ser 3-30 characteres"});
        }
        if(!IsValidPassword(request.Password)){
            return BadRequest(new{message = "senha deve ser de 8 a 40 characteres"});
        }
        //valida existencia
        var usernameExists = _context.Users.Any(u => u.Username == username);
        if(usernameExists){
            return BadRequest(new{ message = "Usuário já cadastrado" });
        }

        var user = new User{
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "user",
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(new{message = "Usuário criado com sucesso"});
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO request){
        var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
        if(user == null || !BCrypt.Net.BCrypt.Verify(request.Password,user.PasswordHash)){
            return Unauthorized(new{message = "Usuário ou senha incorretos"});
        }
        var token = _tokenService.GenerateToken(user);

        return Ok(new{token,user = new{user.Id,user.Username,user.Role}});
    }

    [Authorize]
    [HttpGet("me")] 
    public IActionResult Me(){
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        var username = User.FindFirstValue(JwtRegisteredClaimNames.UniqueName) ?? User.FindFirstValue(ClaimTypes.Name);
        var role = User.FindFirstValue(ClaimTypes.Role);

        return Ok(new{id = userId,username,role});
    }

    private static bool IsValidPassword(string password){
        if(password.Length < 8 || password.Length > 40){
            return false;
        }
        return true;
    }

}