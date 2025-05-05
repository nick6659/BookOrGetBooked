using BookOrGetBooked.App.Shared.Interfaces;

namespace BookOrGetBooked.App.Web.Services;

public class WebTokenStorage : ITokenStorage
{
    private string? _token;

    public Task SaveTokenAsync(string token)
    {
        _token = token;
        return Task.CompletedTask;
    }

    public Task<string?> GetTokenAsync()
    {
        return Task.FromResult(_token);
    }

    public Task ClearTokenAsync()
    {
        _token = null;
        return Task.CompletedTask;
    }
}
