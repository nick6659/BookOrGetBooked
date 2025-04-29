using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Services;
using BookOrGetBooked.Infrastructure.Data.Repositories;
using BookOrGetBooked.Infrastructure.Data;
using BookOrGetBooked.API.Mappings;
using BookOrGetBooked.Infrastructure.Data.SeedData.SeedServices;
using Microsoft.EntityFrameworkCore;
using BookOrGetBooked.Infrastructure.ExternalServices;

namespace BookOrGetBooked.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure built-in logging
            builder.Logging.ClearProviders();  // Remove default loggers
            builder.Logging.AddDebug();        // Debug output
            builder.Logging.AddConsole();      // Console output

            // Add logging to DI container
            builder.Services.AddLogging();

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure JWT Authentication
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Optional: Remove default 5-minute leeway
                };
            });

            // Dependency Injection for repositories and services
            builder.Services.AddScoped<UserManager<ApplicationUser>>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
            builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();
            builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();

            builder.Services.AddHttpClient<IGoogleDistanceService, GoogleDistanceService>();

            // Register centralized DataSeederService
            builder.Services.AddScoped<DataSeederService>();

            // Add AutoMapper and register the base profile
            builder.Services.AddAutoMapper(typeof(MappingProfileBase));

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Retrieve the logger from DI
            var logger = app.Services.GetRequiredService<ILogger<Program>>();

            try
            {
                logger.LogInformation("Starting the application...");
                LogToFile("Starting the application...");

                // Apply migrations and seed data
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    try
                    {
                        dbContext.Database.Migrate();
                        var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeederService>();
                        dataSeeder.SeedAll();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error during migration or seeding.");
                        LogToFile($"Error during migration or seeding: {ex}");
                    }
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

                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Application failed to start.");
                LogToFile($"Critical error on startup: {ex}");
            }
        }

        private static void LogToFile(string message)
        {
            string logFilePath = "logs/app-errors.log";
            string? logDirectory = Path.GetDirectoryName(logFilePath);

            if (!string.IsNullOrEmpty(logDirectory) && !Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}
