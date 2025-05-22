namespace BookOrGetBooked.App.Shared.Constants;

public static class HttpRequestOptionsKeys
{
    public static readonly HttpRequestOptionsKey<string?> AccessToken = new("AccessToken");
    public static readonly HttpRequestOptionsKey<string?> RefreshToken = new("RefreshToken");
}
