using BookOrGetBooked.App.Shared.Constants;
using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.ServiceType;
using System.Net.Http.Json;

namespace BookOrGetBooked.App.Client.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;

        public ServiceTypeService(IHttpClientFactory factory, ITokenStorage tokenStorage)
        {
            _httpClient = factory.CreateClient(nameof(IServiceTypeService));
            _tokenStorage = tokenStorage;
        }

        public async Task<List<ServiceTypeResponseDTO>> GetAvailableForUserAsync()
        {
            var accessToken = await _tokenStorage.GetAccessTokenAsync();
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/servicetype/available");
            request.Options.Set(HttpRequestOptionsKeys.AccessToken, accessToken);
            request.Options.Set(HttpRequestOptionsKeys.RefreshToken, refreshToken);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<ServiceTypeResponseDTO>>() ?? new();
        }

        public async Task<ServiceTypeResponseDTO?> CreateAsync(ServiceTypeCreateDTO dto)
        {
            var accessToken = await _tokenStorage.GetAccessTokenAsync();
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Post, "api/servicetype")
            {
                Content = JsonContent.Create(dto)
            };

            request.Options.Set(HttpRequestOptionsKeys.AccessToken, accessToken);
            request.Options.Set(HttpRequestOptionsKeys.RefreshToken, refreshToken);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ServiceTypeResponseDTO>();
        }
    }
}
