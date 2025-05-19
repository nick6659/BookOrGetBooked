using BookOrGetBooked.Infrastructure.Data;
using System.Security.Claims;

namespace BookOrGetBooked.Core.Interfaces;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
