using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QMan.Application.Dtos.Base;
using static System.Enum;

namespace QMan.Infrastructure.Helpers;

public static class JwtHelper
{
    public static string GenerateToken(UserJwtModel model, string key)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()),
                new Claim(ClaimTypes.Role, GetName(model.Role) ?? "User")
            }),
            // Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static UserJwtModel GetUser(string token)
    {
        var user = new UserJwtModel();
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);

        user.UserId = int.Parse(jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)
            ?.Value ?? "0");

        user.Role = Parse<UserRole>(
            jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value ?? "User");

        return user;
    }
}