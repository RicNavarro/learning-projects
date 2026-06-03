using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

using OrderFlow.Api.DTOs.Requests;
using OrderFlow.Api.DTOs.Responses;
using OrderFlow.Api.Services.Interfaces;

namespace OrderFlow.Api.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public LoginResponse Login(LoginRequest request)
    {
        if(request.Email != "admin@orderflow.com" || request.Password != "001122")
        {
            throw new UnauthorizedAccessException("Credenciais inválidas");
        }

        var token = GenerateToken(request.Email);

        return new LoginResponse
        {
            Token = token
        };
    }

    private string GenerateToken(string email)
    {
        var key = _configuration["Jwt:Key"];

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key!)
        );

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256
        );

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

}