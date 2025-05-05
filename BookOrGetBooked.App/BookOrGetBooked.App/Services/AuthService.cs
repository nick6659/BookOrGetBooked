using System.Net.Http.Headers;
using System.Net.Http.Json;
using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.App.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;

        public AuthService(HttpClient httpClient, ITokenStorage tokenStorage)
        {
            _httpClient = httpClient;
            _tokenStorage = tokenStorage;
        }

        public async Task<(bool Success, string? ErrorMessage)> LoginAsync(LoginRequestDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);

            if (!response.IsSuccessStatusCode)
            {
                return (false, "Invalid login credentials.");
            }

            var json = await response.Content.ReadFromJsonAsync<TokenResponseDto>();
            if (json is null || string.IsNullOrEmpty(json.Token))
            {
                return (false, "No token received.");
            }

            await _tokenStorage.SaveTokenAsync(json.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", json.Token);

            return (true, null);
        }

        public async Task LogoutAsync()
        {
            await _tokenStorage.ClearTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> TryRestoreSessionAsync()
        {
            var token = await _tokenStorage.GetTokenAsync();

            if (string.IsNullOrWhiteSpace(token))
                return false;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }
    }
}
