using BookOrGetBooked.App.Shared.Constants;
using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.Service;
using System.Net.Http.Json;

namespace BookOrGetBooked.App.Client.Services
{
    public class ServiceService : IServiceService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;

        public ServiceService(IHttpClientFactory httpClientFactory, ITokenStorage tokenStorage)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(IServiceService));
            _tokenStorage = tokenStorage;
        }

        public async Task<List<ServiceResponseDTO>> GetServicesByProviderAsync(string providerId)
        {
            var accessToken = await _tokenStorage.GetAccessTokenAsync();
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            var filterPayload = new
            {
                providerId = providerId,
                includeDeleted = false,
                isInactive = (bool?)null,
                startDate = (DateTime?)null,
                endDate = (DateTime?)null
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/service/filter")
            {
                Content = JsonContent.Create(filterPayload)
            };

            request.Options.Set(HttpRequestOptionsKeys.AccessToken, accessToken);
            request.Options.Set(HttpRequestOptionsKeys.RefreshToken, refreshToken);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ServiceResponseDTO>>() ?? new();
            }

            throw new HttpRequestException("Failed to load services for provider.");
        }

        public async Task<bool> CreateServiceAsync(ServiceCreateDTO dto)
        {
            var accessToken = await _tokenStorage.GetAccessTokenAsync();
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Post, "api/service")
            {
                Content = JsonContent.Create(dto)
            };

            request.Options.Set(HttpRequestOptionsKeys.AccessToken, accessToken);
            request.Options.Set(HttpRequestOptionsKeys.RefreshToken, refreshToken);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return true;

            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Failed to create service: {error}");
        }
    }
}
