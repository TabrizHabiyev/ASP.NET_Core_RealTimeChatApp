using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealTimeChatApp.Application.Common.Interfaces.Token;
using RealTimeChatApp.Domain.Entities;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealTimeChatApp.Infrastructure.Services.Token;

public class TokenHadler : ITokenHandler
{
    private readonly IConfiguration _configuration;
    public TokenHadler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Application.DTOs.Token CreateAccessToken(int expiration, User user, IList<string> roles)
    {

        Application.DTOs.Token token = new();

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
            {
              new Claim (ClaimTypes.Name,user.UserName),
              new Claim(ClaimTypes.Sid, user.Id.ToString())
            };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


        token.Expiration = DateTime.UtcNow.AddDays(expiration);
        JwtSecurityToken securityToken = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            claims: claims,
            signingCredentials: signingCredentials
            );

        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        return token;
    }
}
