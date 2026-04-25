using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SysClaim = System.Security.Claims.Claim;
using System.Text;
using InsureTrust.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace InsureTrust.API.Helpers;

public static class NumberGenerators
{
    public static string GenerateUserNumber(int id) => $"USR{(id + 1000):D4}";
    public static string GeneratePolicyNumber(int id) => $"POL{(id + 2000):D4}";
    public static string GenerateTicketNumber(int id) => $"SUP{(id + 3000):D4}";
    public static string GenerateClaimNumber(int id) => $"CLM{(id + 4000):D4}";
    public static string GeneratePaymentNumber(int id) => $"PAY{(id + 5000):D4}";
}

public class JwtHelper
{
    private readonly IConfiguration _config;

    public JwtHelper(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new SysClaim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new SysClaim(ClaimTypes.Email, user.Email),
            new SysClaim(ClaimTypes.Name, user.Name),
            new SysClaim(ClaimTypes.Role, user.Role),
            new SysClaim("UserNumber", user.UserNumber)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
