using BookOrGetBooked.App.Shared.Constants;
using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.Auth;
using BookOrGetBooked.Shared.DTOs.General;
using BookOrGetBooked.Shared.DTOs.User;
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

            var request = new HttpRequestMessage(HttpMethod.Get, "api/profile/me");
            request.Options.Set(HttpRequestOptionsKeys.AccessToken, accessToken);
            request.Options.Set(HttpRequestOptionsKeys.RefreshToken, refreshToken);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> TryRefreshTokenAsync()
        {
            var accessToken = await _tokenStorage.GetAccessTokenAsync();
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            if (string.IsNullOrWhiteSpace(accessToken) || string.IsNullOrWhiteSpace(refreshToken))
                return false;

            var request = new HttpRequestMessage(HttpMethod.Post, "api/auth/refresh-token")
            {
                Content = JsonContent.Create(new RefreshTokenRequestDto
                {
                    Token = accessToken,
                    RefreshToken = refreshToken
                })
            };

            request.Options.Set(HttpRequestOptionsKeys.AccessToken, accessToken);
            request.Options.Set(HttpRequestOptionsKeys.RefreshToken, refreshToken);

            var response = await _httpClient.SendAsync(request);

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
            var accessToken = await _tokenStorage.GetAccessTokenAsync();
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            if (string.IsNullOrWhiteSpace(accessToken))
                return null;

            var request = new HttpRequestMessage(HttpMethod.Get, "api/auth/me");
            request.Options.Set(HttpRequestOptionsKeys.AccessToken, accessToken);
            request.Options.Set(HttpRequestOptionsKeys.RefreshToken, refreshToken);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<CurrentUserDTO>();
        }

    }
}
