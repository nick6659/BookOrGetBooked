using Blazored.LocalStorage;
using BookOrGetBooked.App.Client.Services;
using BookOrGetBooked.App.Client.Services.Http;
using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.App.Web;
using BookOrGetBooked.App.Web.Components;
using BookOrGetBooked.App.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookOrGetBooked.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddBlazoredLocalStorage();

            var config = builder.Configuration;
            var apiBaseUrl = config["ApiSettings:BaseUrl"]
                ?? throw new InvalidOperationException("Missing 'ApiSettings:BaseUrl' in appsettings.json.");

            builder.Services.AddScoped<ITokenStorage, WebTokenStorage>();
            builder.Services.AddSingleton<JwtParserService>();

            builder.Services.AddTransient<RefreshTokenHandler>();

            builder.Services.AddHttpClient(nameof(IAuthService), client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            })
            .AddHttpMessageHandler<RefreshTokenHandler>();

            builder.Services.AddHttpClient(nameof(IBookingService), client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });
            builder.Services.AddScoped<IBookingService, BookingService>();

            builder.Services.AddHttpClient(nameof(IUserProfileService), client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            }).AddHttpMessageHandler<RefreshTokenHandler>();

            builder.Services.AddScoped<IUserProfileService, UserProfileService>();

            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddAuthentication("FakeScheme")
                .AddScheme<AuthenticationSchemeOptions, FakeAuthHandler>("FakeScheme", _ => { });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorComponents<AppRoot>()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(typeof(BookOrGetBooked.App.Shared._Imports).Assembly);

            app.Run();
        }
    }
}
