using BookOrGetBooked.Shared.DTOs.Auth;
using BookOrGetBooked.Shared.Utilities;

namespace BookOrGetBooked.Core.Interfaces;

public interface IAuthService
{
    Task<Result> RegisterAsync(RegisterRequestDto model, CancellationToken cancellationToken = default);
    Task<Result<TokenResponseDto>> LoginAsync(LoginRequestDto model);
    Task<Result<TokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto model);
}
