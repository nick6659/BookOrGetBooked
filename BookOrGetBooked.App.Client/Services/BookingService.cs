using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.Booking;
using BookOrGetBooked.Shared.DTOs.Service;
using System.Net.Http.Json;

namespace BookOrGetBooked.App.Client.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _httpClient;

        public BookingService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(IBookingService));
        }

        public async Task<List<ServiceResponseDTO>> GetAvailableServicesAsync()
        {
            var request = new
            {
                userId = 0,
                includeDeleted = false,
                isInactive = (bool?)null,
                startDate = (DateTime?)null,
                endDate = (DateTime?)null
            };

            var response = await _httpClient.PostAsJsonAsync("api/service/filter", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ServiceResponseDTO>>() ?? new();
            }

            throw new HttpRequestException("Failed to load services.");
        }

        public async Task CreateBookingAsync(BookingCreateDTO booking)
        {
            var response = await _httpClient.PostAsJsonAsync("api/booking", booking);
            response.EnsureSuccessStatusCode(); // Will throw if not 2xx
        }
    }
}
