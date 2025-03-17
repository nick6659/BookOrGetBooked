using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Services;
using BookOrGetBooked.Infrastructure.Data.Repositories;
using BookOrGetBooked.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BookOrGetBooked.API.Mappings;
using BookOrGetBooked.Infrastructure.Data.SeedData.SeedServices;

namespace BookOrGetBooked.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add DbContext with SQLite for now
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Dependency Injection for repositories and services
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();
            builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            builder.Services.AddScoped<IUserTypeService, UserTypeService>();

            // Register only the centralized DataSeederService
            builder.Services.AddScoped<DataSeederService>();

            // Add AutoMapper and register the base profile
            builder.Services.AddAutoMapper(typeof(MappingProfileBase));

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Apply migrations and seed data
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Apply migrations first
                dbContext.Database.Migrate();

                // Call centralized DataSeederService to seed all necessary data
                var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeederService>();
                dataSeeder.SeedAll();  // This triggers all the individual seed services
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.Urls.Add("https://app.bookorgetbooked.com");
            app.Urls.Add("https://localhost:5001");
            app.Urls.Add("http://localhost:5000");

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
