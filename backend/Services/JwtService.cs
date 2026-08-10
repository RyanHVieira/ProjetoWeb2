using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.services;

public class JwtService{
    private readonly IConfiguration _config;
    public JwtService(IConfiguration config){
        _config = config;
    }
    public string GenerateToken(User user){
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        var claims = new[]{
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim("role", user.Role)
        };
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresInMinutes"]!));
        var token = new JwtSecurityToken(issuer: _config["Jwt:Issuer"],audience: _config["Jwt:Audience"],claims: claims,expires: expires,signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}