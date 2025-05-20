using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.Auth;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace BookOrGetBooked.App.Client.Services.Http;

public class RefreshTokenHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokenStorage;
    private readonly IHttpClientFactory _httpClientFactory;

    public RefreshTokenHandler(ITokenStorage tokenStorage, IHttpClientFactory httpClientFactory)
    {
        _tokenStorage = tokenStorage;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            string? accessToken = null;
            try
            {
                accessToken = await _tokenStorage.GetAccessTokenAsync();
                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Token unavailable (likely prerender): " + ex.Message);
            }

            var response = await base.SendAsync(request, cancellationToken);

            // If Unauthorized, Try Refresh Token
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                try
                {
                    var refreshToken = await _tokenStorage.GetRefreshTokenAsync();
                    if (!string.IsNullOrWhiteSpace(refreshToken) && !string.IsNullOrWhiteSpace(accessToken))
                    {
                        var refreshResponse = await TryRefreshTokenAsync(accessToken, refreshToken);

                        if (!string.IsNullOrWhiteSpace(refreshResponse?.Token) &&
                            !string.IsNullOrWhiteSpace(refreshResponse.RefreshToken))
                        {
                            await _tokenStorage.SaveTokensAsync(refreshResponse.Token, refreshResponse.RefreshToken);

                            // Retry original request with new token
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshResponse.Token);
                            response = await base.SendAsync(request, cancellationToken);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Refresh failed: " + ex.Message);
                }
            }

            return response;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("API connection failed: " + ex.Message);
            throw new InvalidOperationException("Unable to reach the server. Please try again later.", ex);
        }
    }

    private async Task<TokenResponseDto?> TryRefreshTokenAsync(string expiredToken, string refreshToken)
    {
        try
        {
            var client = _httpClientFactory.CreateClient(nameof(IAuthService));

            var refreshRequest = new RefreshTokenRequestDto
            {
                Token = expiredToken,
                RefreshToken = refreshToken
            };

            var response = await client.PostAsJsonAsync("api/auth/refresh-token", refreshRequest);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TokenResponseDto>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Refresh token failed: " + ex.Message);
        }

        return null;
    }
}
