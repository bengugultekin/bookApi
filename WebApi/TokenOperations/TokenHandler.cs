using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.TokenOperations;

namespace WebApi;

public class TokenHandler
{
    public IConfiguration Configuration { get; set; }
    public TokenHandler(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public Token CreateAccessToken(User user)
    {
        Token tokenModel = new Token();
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        tokenModel.Expiration = DateTime.Now.AddDays(15);

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: Configuration["Jwt:Issuer"],
            audience: Configuration["Jwt:Audience"],
            expires: tokenModel.Expiration,
            notBefore: DateTime.Now,
            signingCredentials: credentials
            );

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        // Creating Token
        tokenModel.AccessToken = tokenHandler.WriteToken( securityToken );
        tokenModel.RefreshToken = CreateRefreshToken();

        return tokenModel;
    }
    
    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
