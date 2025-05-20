using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.General;
using BookOrGetBooked.Shared.DTOs.Profile;
using System.Net.Http.Json;

public class UserProfileService : IUserProfileService
{
    private readonly HttpClient _http;

    public UserProfileService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient(nameof(IUserProfileService));
    }

    public async Task<UserProfileResponseDto?> GetProfileAsync()
    {
        var result = await _http.GetFromJsonAsync<ResultDto<UserProfileResponseDto>>("api/profile/me");
        return result?.IsSuccess == true ? result.Data : null;
    }

    public async Task<bool> UpdateProfileAsync(UpdateUserProfileDto dto)
    {
        var response = await _http.PutAsJsonAsync("api/profile", dto);
        return response.IsSuccessStatusCode;
    }
}
