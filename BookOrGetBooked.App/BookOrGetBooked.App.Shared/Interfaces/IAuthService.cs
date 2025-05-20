using BookOrGetBooked.Shared.DTOs.Auth;
using BookOrGetBooked.Shared.DTOs.User;

namespace BookOrGetBooked.App.Shared.Interfaces;

public interface IAuthService
{
    Task<(bool IsSuccess, string? ErrorMessage)> LoginAsync(LoginRequestDto loginDto);
    Task<(bool IsSuccess, string? ErrorMessage)> RegisterAsync(RegisterRequestDto registerDto);
    Task LogoutAsync();
    /// <summary>
    /// Tries to restore the user session using a stored access token, or a refresh token if expired.
    /// </summary>
    Task<bool> TryRestoreSessionAsync();

    /// <summary>
    /// Attempts to use the refresh token to get a new access token.
    /// </summary>
    Task<bool> TryRefreshTokenAsync();

    Task<CurrentUserDTO?> GetCurrentUserAsync();
}
