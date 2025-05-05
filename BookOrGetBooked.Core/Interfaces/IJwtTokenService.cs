using BookOrGetBooked.Infrastructure.Data;

namespace BookOrGetBooked.Core.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser user);
}
