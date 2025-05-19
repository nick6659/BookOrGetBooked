
namespace BookOrGetBooked.App.Shared.Interfaces
{
    public interface ITokenStorage
    {
        Task SaveTokensAsync(string accessToken, string refreshToken);
        Task<string?> GetAccessTokenAsync();
        Task<string?> GetRefreshTokenAsync();
        Task ClearTokensAsync();
        Task TryFlushPendingTokenWritesAsync();
    }
}
