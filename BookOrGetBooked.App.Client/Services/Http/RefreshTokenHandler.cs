using BookOrGetBooked.App.Shared.Constants;
using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.Auth;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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
        request.Options.TryGetValue(HttpRequestOptionsKeys.AccessToken, out var accessToken);
        request.Options.TryGetValue(HttpRequestOptionsKeys.RefreshToken, out var refreshToken);

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // If Unauthorized, try refreshing token
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (!string.IsNullOrWhiteSpace(accessToken) && !string.IsNullOrWhiteSpace(refreshToken))
            {
                var refreshResponse = await TryRefreshTokenAsync(accessToken, refreshToken);
                if (!string.IsNullOrWhiteSpace(refreshResponse?.Token) && !string.IsNullOrWhiteSpace(refreshResponse.RefreshToken))
                {
                    await _tokenStorage.SaveTokensAsync(refreshResponse.Token, refreshResponse.RefreshToken);

                    // Retry original request with new token
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshResponse.Token);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }
        }

        return response;
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
