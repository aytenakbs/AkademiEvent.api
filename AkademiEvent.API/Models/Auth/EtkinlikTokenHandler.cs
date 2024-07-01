using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AkademiEvent.API.Models.Auth;

public class EtkinlikTokenHandler
{
    public static JWTToken CreateToken(string email)
    {
        var responseModel = new JWTToken();
        var claim = new[]
        {
            new Claim(ClaimTypes.Email, email), 
        };
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ironmaidenpentagramslipknotironmaidenpentagramslipknot"));
        SigningCredentials credentials=new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        responseModel.AccessTokenExpiration=System.DateTime.Now.AddMinutes(30);
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer:"ayten@gmail.com",
            audience: "aytenn@gmail.com",
            expires:responseModel.AccessTokenExpiration,
            signingCredentials:credentials,
            claims:claim
            );
        JwtSecurityTokenHandler tokenHandler=new JwtSecurityTokenHandler();
        responseModel.AccessToken=tokenHandler.WriteToken(jwtSecurityToken);
        return responseModel;
    }
}
