using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.Service;
using System.Net.Http.Json;

namespace BookOrGetBooked.App.Client.Services
{
    public class ServiceService : IServiceService
    {
        private readonly HttpClient _httpClient;

        public ServiceService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(IServiceService));
        }

        public async Task<List<ServiceResponseDTO>> GetServicesByProviderAsync(string providerId)
        {
            var request = new
            {
                providerId = providerId,
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

            throw new HttpRequestException("Failed to load services for provider.");
        }

        public async Task<bool> CreateServiceAsync(ServiceCreateDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/service", dto);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Failed to create service: {error}");
        }

    }
}
