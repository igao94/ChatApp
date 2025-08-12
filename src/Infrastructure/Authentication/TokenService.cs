using Application.Abstractions.Authentication;
using Domain.Entites;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;

internal sealed class TokenService(IConfiguration config) : ITokenService
{
    public string GetToken(AppUser user)
    {
        var creds = GetCredentials();

        var claims = GetClaims(user);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"],
            SigningCredentials = creds,
            Expires = DateTime.UtcNow.AddDays(1)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private SigningCredentials GetCredentials()
    {
        var tokenKey = config["Jwt:TokenKey"] ?? throw new Exception("Can't access token key.");

        if (tokenKey.Length < 64)
        {
            throw new Exception("TokenKey needs to be longer.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        return new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    }

    private static List<Claim> GetClaims(AppUser user)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email)
        ];

        return claims;
    }
}
