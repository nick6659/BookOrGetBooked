using BookOrGetBooked.App.Shared.Constants;
using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.BookingStatus;
using System.Net.Http.Json;

namespace BookOrGetBooked.App.Client.Services
{
    public class BookingStatusService : IBookingStatusService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;

        public BookingStatusService(IHttpClientFactory httpClientFactory, ITokenStorage tokenStorage)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(IBookingStatusService));
            _tokenStorage = tokenStorage;
        }

        public async Task<List<BookingStatusSummaryDTO>> GetAllAsync()
        {
            var accessToken = await _tokenStorage.GetAccessTokenAsync();
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, "api/bookingstatus");
            request.Options.Set(HttpRequestOptionsKeys.AccessToken, accessToken);
            request.Options.Set(HttpRequestOptionsKeys.RefreshToken, refreshToken);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var statuses = await response.Content.ReadFromJsonAsync<List<BookingStatusSummaryDTO>>();
            return statuses ?? new();
        }
    }
}
