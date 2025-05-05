using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.App.Web.Components;
using BookOrGetBooked.App.Web.Services;

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

            builder.Services.AddScoped<ITokenStorage, WebTokenStorage>();

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

            app.MapRazorComponents<AppRoot>()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(typeof(BookOrGetBooked.App.Shared._Imports).Assembly);

            app.Run();
        }
    }
}
