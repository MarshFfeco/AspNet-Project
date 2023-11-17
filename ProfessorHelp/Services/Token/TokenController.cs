using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProfessorHelp.Services.Token;

public class TokenController
{
    private const string EmailAlias = "eml";
    private readonly double _lifeToken;
    private readonly string _SecurityKey;

    public TokenController(double lifeToken, string securityKey)
    {
        _lifeToken = lifeToken;
        _SecurityKey = securityKey;
    }

    public string GenerateToken(string userEmail)
    {
        var claims = new List<Claim>
        {
            new Claim(EmailAlias, userEmail),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes( _lifeToken ),
            SigningCredentials = new SigningCredentials(SimetricKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(securityToken);
    }

    public void ValidateToken(string token)
    {
        JwtSecurityTokenHandler tokenHandledr = new JwtSecurityTokenHandler();

        TokenValidationParameters paramsValidate = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SimetricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        tokenHandledr.ValidateToken(token, paramsValidate, out _);
    }

    private SymmetricSecurityKey SimetricKey()
    {
        var symmetricKey = Convert.FromBase64String( _SecurityKey );
        return new SymmetricSecurityKey( symmetricKey );
    }
}
