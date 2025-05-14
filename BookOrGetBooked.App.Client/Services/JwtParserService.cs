using System.IdentityModel.Tokens.Jwt;

namespace BookOrGetBooked.App.Client.Services
{
    public class JwtParserService
    {
        public Dictionary<string, string> ParseClaimsFromJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            return jwt.Claims.ToDictionary(c => c.Type, c => c.Value);
        }
    }
}
