using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.Booking;
using BookOrGetBooked.Shared.DTOs.General;
using BookOrGetBooked.Shared.DTOs.Service;
using BookOrGetBooked.Shared.Filters;
using BookOrGetBooked.Shared.Utilities;
using Microsoft.Extensions.Logging;
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

        public async Task<ResultDto<BookingSummaryDTO>> CreateBookingAsync(BookingCreateDTO booking)
        {
            var response = await _httpClient.PostAsJsonAsync("api/booking", booking);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ResultDto<BookingSummaryDTO>>();

            if (result == null)
                throw new Exception("Failed to deserialize booking result.");

            return result;
        }

        public async Task<List<BookingSummaryDTO>> GetBookingsForServiceAsync(int serviceId)
        {
            var filter = new BookingFilterParameters
            {
                ServiceId = serviceId
            };

            var response = await _httpClient.PostAsJsonAsync("api/booking/filter", filter);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ResultDto<IEnumerable<BookingSummaryDTO>>>();
            return result?.Data?.ToList() ?? new();
        }

        public async Task<ResultDto<BookingSummaryDTO>> UpdateBookingAsync(int id, BookingUpdateDTO updateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/booking/{id}", updateDto);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ResultDto<BookingSummaryDTO>>();
            if (result == null)
                throw new Exception("Failed to deserialize updated booking result.");

            return result;
        }

        public async Task UpdateBookingAsProviderAsync(int bookingId, ServiceProviderBookingUpdateDTO updateDto)
        {
            var response = await _httpClient.PatchAsJsonAsync($"api/booking/{bookingId}/provider", updateDto);
            response.EnsureSuccessStatusCode();
        }

    }
}
