using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.BookingStatus;
using System.Net.Http.Json;

namespace BookOrGetBooked.App.Client.Services
{
    public class BookingStatusService : IBookingStatusService
    {
        private readonly HttpClient _httpClient;

        public BookingStatusService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(IBookingStatusService));
        }

        public async Task<List<BookingStatusSummaryDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/bookingstatus");
            response.EnsureSuccessStatusCode();

            var statuses = await response.Content.ReadFromJsonAsync<List<BookingStatusSummaryDTO>>();
            return statuses ?? new();
        }
    }
}
