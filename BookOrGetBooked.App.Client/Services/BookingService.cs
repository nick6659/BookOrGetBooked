using BookOrGetBooked.App.Shared.Constants;
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
        private readonly ITokenStorage _tokenStorage;

        public BookingService(IHttpClientFactory httpClientFactory, ITokenStorage tokenStorage)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(IBookingService));
            _tokenStorage = tokenStorage;
        }

        public async Task<List<ServiceResponseDTO>> GetAvailableServicesAsync()
        {
            var requestData = new
            {
                userId = 0,
                includeDeleted = false,
                isInactive = (bool?)null,
                startDate = (DateTime?)null,
                endDate = (DateTime?)null
            };

            var request = await CreateAuthorizedRequestAsync(HttpMethod.Post, "api/service/filter", requestData);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ServiceResponseDTO>>() ?? new();
            }

            throw new HttpRequestException("Failed to load services.");
        }

        public async Task<ResultDto<BookingSummaryDTO>> CreateBookingAsync(BookingCreateDTO booking)
        {
            var request = await CreateAuthorizedRequestAsync(HttpMethod.Post, "api/booking", booking);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ResultDto<BookingSummaryDTO>>();

            if (result == null)
                throw new Exception("Failed to deserialize booking result.");

            return result;
        }

        public async Task<List<BookingSummaryDTO>> GetBookingsForServiceAsync(int serviceId)
        {
            var filter = new BookingFilterParameters { ServiceId = serviceId };
            var request = await CreateAuthorizedRequestAsync(HttpMethod.Post, "api/booking/filter", filter);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ResultDto<IEnumerable<BookingSummaryDTO>>>();

            if (result is null || !result.IsSuccess || result.Data is null)
                throw new Exception("Failed to fetch bookings for service.");

            return result.Data.ToList();
        }

        public async Task<ResultDto<BookingSummaryDTO>> UpdateBookingAsync(int id, BookingUpdateDTO updateDto)
        {
            var request = await CreateAuthorizedRequestAsync(HttpMethod.Put, $"api/booking/{id}", updateDto);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ResultDto<BookingSummaryDTO>>();
            if (result == null)
                throw new Exception("Failed to deserialize updated booking result.");

            return result;
        }

        public async Task UpdateBookingAsProviderAsync(int bookingId, ServiceProviderBookingUpdateDTO updateDto)
        {
            var request = await CreateAuthorizedRequestAsync(HttpMethod.Patch, $"api/booking/{bookingId}/provider", updateDto);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        private async Task<HttpRequestMessage> CreateAuthorizedRequestAsync(HttpMethod method, string uri, object? content = null)
        {
            var accessToken = await _tokenStorage.GetAccessTokenAsync();
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            var request = new HttpRequestMessage(method, uri);
            request.Options.Set(HttpRequestOptionsKeys.AccessToken, accessToken);
            request.Options.Set(HttpRequestOptionsKeys.RefreshToken, refreshToken);

            if (content != null)
            {
                request.Content = JsonContent.Create(content);
            }

            return request;
        }
    }
}
