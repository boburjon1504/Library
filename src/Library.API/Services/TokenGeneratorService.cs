using Library.API.Services.Interfaces;
using Library.Models.Common.Settings;
using Library.Models.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.API.Services;

public class TokenGeneratorService(IOptions<JwtSettings> jwtSettings) : ITokenGeneratorService
{
    private JwtSettings _jwtSettings = jwtSettings.Value;
    public string GenerateToken(User user)
    {
        var jwtToken = GetJwtToken(user);

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    private JwtSecurityToken GetJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = GetClaims(user);


        return new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes),
            signingCredentials: credentials
            );
    }

    private IList<Claim> GetClaims(User user)
    {
        return new List<Claim>()
        {
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("UserId", user.Id.ToString())
        };
    }
}
