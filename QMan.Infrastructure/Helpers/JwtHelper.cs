using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using QMan.Application.Dtos.Base;
using static System.Enum;

namespace QMan.Infrastructure.Helpers;

public static class JwtHelper
{
    public static string GenerateToken(UserJwtModel model)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // Subject = new ClaimsIdentity(new[]
            // {
            //     new Claim("UserId", model.UserId.ToString()),
            //     new Claim(ClaimTypes.Role, GetName(model.Role) ?? "User")
            // }),
            Claims = new Dictionary<string, object>
            {
                { "UserId", model.UserId.ToString() },
                { ClaimTypes.Role, GetName(model.Role) ?? "User" }
            },
            Audience = ConfigurationModel.Instance.Audience,
            Issuer = ConfigurationModel.Instance.Issuer,
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigurationModel.Instance.JwtToken)),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static UserJwtModel? GetUser(string? token)
    {
        if (string.IsNullOrWhiteSpace(token)) return null;
        var user = new UserJwtModel();
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);

        user.UserId = int.Parse(jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")
            ?.Value ?? "0");

        user.Role = Parse<UserRole>(
            jwtSecurityToken.Claims
                .FirstOrDefault(claim => claim.Type.Equals("role", StringComparison.CurrentCultureIgnoreCase))?.Value ??
            "User");

        return user;
    }
}