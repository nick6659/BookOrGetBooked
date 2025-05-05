using BookOrGetBooked.App.Shared.Interfaces;
using Microsoft.Maui.Storage;

namespace BookOrGetBooked.App.Services
{
    public class MauiTokenStorage : ITokenStorage
    {
        private const string TokenKey = "auth_token";

        public Task SaveTokenAsync(string token)
        {
            SecureStorage.SetAsync(TokenKey, token);
            return Task.CompletedTask;
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                return await SecureStorage.GetAsync(TokenKey);
            }
            catch
            {
                // SecureStorage might throw if not supported
                return null;
            }
        }

        public Task ClearTokenAsync()
        {
            SecureStorage.Remove(TokenKey);
            return Task.CompletedTask;
        }
    }
}
