using Blazored.LocalStorage;
using BookOrGetBooked.App.Shared.Interfaces;

namespace BookOrGetBooked.App.Web.Services;

public class WebTokenStorage : ITokenStorage
{
    private const string AccessTokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";

    private readonly ILocalStorageService _localStorage;

    // Temporarily store in memory until JS interop is safe
    private string? _queuedAccessToken;
    private string? _queuedRefreshToken;

    public WebTokenStorage(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task SaveTokensAsync(string accessToken, string refreshToken)
    {
        // Try to save immediately
        try
        {
            await _localStorage.SetItemAsync(AccessTokenKey, accessToken);
            await _localStorage.SetItemAsync(RefreshTokenKey, refreshToken);
        }
        catch
        {
            // If interop isn't ready, queue the tokens
            _queuedAccessToken = accessToken;
            _queuedRefreshToken = refreshToken;
        }
    }

    public async Task TryFlushQueuedTokensAsync()
    {
        if (_queuedAccessToken is not null && _queuedRefreshToken is not null)
        {
            await _localStorage.SetItemAsync(AccessTokenKey, _queuedAccessToken);
            await _localStorage.SetItemAsync(RefreshTokenKey, _queuedRefreshToken);
            _queuedAccessToken = null;
            _queuedRefreshToken = null;
        }
    }

    public async Task<string?> GetAccessTokenAsync() =>
        await _localStorage.GetItemAsync<string>(AccessTokenKey);

    public async Task<string?> GetRefreshTokenAsync() =>
        await _localStorage.GetItemAsync<string>(RefreshTokenKey);

    public async Task ClearTokensAsync()
    {
        await _localStorage.RemoveItemAsync(AccessTokenKey);
        await _localStorage.RemoveItemAsync(RefreshTokenKey);
    }

    public Task TryFlushPendingTokenWritesAsync()
    {
        // Wait for JS interop or queue flushing logic
        return Task.CompletedTask;
    }
}
