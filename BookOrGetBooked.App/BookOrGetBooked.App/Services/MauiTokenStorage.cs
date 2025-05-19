using BookOrGetBooked.App.Shared.Interfaces;
using Microsoft.Maui.Storage;

namespace BookOrGetBooked.App.Services
{
    public class MauiTokenStorage : ITokenStorage
    {
        private const string AccessTokenKey = "access_token";
        private const string RefreshTokenKey = "refresh_token";

        public async Task SaveTokensAsync(string accessToken, string refreshToken)
        {
            await SecureStorage.SetAsync(AccessTokenKey, accessToken);
            await SecureStorage.SetAsync(RefreshTokenKey, refreshToken);
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            try
            {
                return await SecureStorage.GetAsync(AccessTokenKey);
            }
            catch
            {
                return null;
            }
        }

        public async Task<string?> GetRefreshTokenAsync()
        {
            try
            {
                return await SecureStorage.GetAsync(RefreshTokenKey);
            }
            catch
            {
                return null;
            }
        }

        public Task ClearTokensAsync()
        {
            SecureStorage.Remove(AccessTokenKey);
            SecureStorage.Remove(RefreshTokenKey);
            return Task.CompletedTask;
        }

        public Task TryFlushPendingTokenWritesAsync()
        {
            // Wait for JS interop or queue flushing logic
            return Task.CompletedTask;
        }
    }
}
