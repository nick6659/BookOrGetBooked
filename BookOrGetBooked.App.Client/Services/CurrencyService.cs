using BookOrGetBooked.App.Shared.Constants;
using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.Currency;
using System.Net.Http.Json;

namespace BookOrGetBooked.App.Client.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;

        public CurrencyService(IHttpClientFactory factory, ITokenStorage tokenStorage)
        {
            _httpClient = factory.CreateClient(nameof(ICurrencyService));
            _tokenStorage = tokenStorage;
        }

        public async Task<List<CurrencyResponseDTO>> GetAllAsync()
        {
            var accessToken = await _tokenStorage.GetAccessTokenAsync();
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, "api/currency");
            request.Options.Set(HttpRequestOptionsKeys.AccessToken, accessToken);
            request.Options.Set(HttpRequestOptionsKeys.RefreshToken, refreshToken);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<CurrencyResponseDTO>>() ?? new();
        }
    }
}
