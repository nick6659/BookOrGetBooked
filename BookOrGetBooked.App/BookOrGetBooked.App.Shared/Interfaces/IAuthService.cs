using BookOrGetBooked.Shared.DTOs.Auth;

namespace BookOrGetBooked.App.Shared.Interfaces;

public interface IAuthService
{
    Task<(bool IsSuccess, string? ErrorMessage)> LoginAsync(LoginRequestDto loginDto);
    Task LogoutAsync();
    Task<bool> TryRestoreSessionAsync();
}
