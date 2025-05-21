using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.Auth;
using BookOrGetBooked.Shared.DTOs.General;
using BookOrGetBooked.Shared.DTOs.User;
using BookOrGetBooked.Shared.Utilities;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BookOrGetBooked.App.Client.Services
{
    public class AuthService : IAuthService, IFlushableAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;

        public AuthService(IHttpClientFactory httpClientFactory, ITokenStorage tokenStorage)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(IAuthService));
            _tokenStorage = tokenStorage;
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> RegisterAsync(RegisterRequestDto registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerDto);

            var result = await response.Content.ReadFromJsonAsync<ResultDto<object>>();

            if (result == null)
                return (false, "Unexpected server response.");

            if (!result.IsSuccess)
            {
                // Prioritize the error message if available
                if (result.Error is not null)
                    return (false, result.Error.Message);

                // If validation errors are present, return the first one
                if (result.ValidationErrors?.Any() == true)
                    return (false, result.ValidationErrors.First().Error);

                return (false, "Registration failed.");
            }

            return (true, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> LoginAsync(LoginRequestDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);

            if (!response.IsSuccessStatusCode)
                return (false, "Invalid login credentials.");

            var resultDto = await response.Content.ReadFromJsonAsync<ResultDto<TokenResponseDto>>();

            if (resultDto is null || !resultDto.IsSuccess || resultDto.Data is null)
                return (false, resultDto?.Error?.Message ?? "Invalid token response.");

            var tokenData = resultDto.Data;

            await _tokenStorage.SaveTokensAsync(tokenData.Token, tokenData.RefreshToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenData.Token);

            return (true, null);
        }

        public async Task LogoutAsync()
        {
            await _tokenStorage.ClearTokensAsync();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> TryRestoreSessionAsync()
        {
            var accessToken = await _tokenStorage.GetAccessTokenAsync();
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            if (string.IsNullOrWhiteSpace(accessToken))
                return false;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var test = await _httpClient.GetAsync("api/profile/me");

            if (test.StatusCode == HttpStatusCode.Unauthorized)
                return await TryRefreshTokenAsync();

            return test.IsSuccessStatusCode;
        }

        public async Task<bool> TryRefreshTokenAsync()
        {
            var accessToken = await _tokenStorage.GetAccessTokenAsync();
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            if (string.IsNullOrWhiteSpace(accessToken) || string.IsNullOrWhiteSpace(refreshToken))
                return false;

            var payload = new RefreshTokenRequestDto
            {
                Token = accessToken,
                RefreshToken = refreshToken
            };

            var response = await _httpClient.PostAsJsonAsync("api/auth/refresh-token", payload);

            if (!response.IsSuccessStatusCode)
                return false;

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponseDto>();
            if (tokenResponse == null || string.IsNullOrWhiteSpace(tokenResponse.Token))
                return false;

            await _tokenStorage.SaveTokensAsync(tokenResponse.Token, tokenResponse.RefreshToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.Token);

            return true;
        }

        public async Task FlushTokenQueueAsync()
        {
            await _tokenStorage.TryFlushPendingTokenWritesAsync();
        }

        public async Task<CurrentUserDTO?> GetCurrentUserAsync()
        {
            var restored = await TryRestoreSessionAsync();
            if (!restored)
                return null;

            return await _httpClient.GetFromJsonAsync<CurrentUserDTO>("api/auth/me");
        }

    }
}
