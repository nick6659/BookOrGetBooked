using Microsoft.EntityFrameworkCore;
using BookOrGetBooked.Core.Models;

namespace BookOrGetBooked.Infrastructure.Data
{
    public class ApplicationDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; } = null!;
        public DbSet<Currency> Currencies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // The relationship between Booking and ApplicationUser (Booker)
            builder.Entity<Booking>()
                .HasOne(b => b.Booker)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.BookerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .HasIndex(b => b.BookerId);

            // The relationship between Booking and Service
            builder.Entity<Booking>()
                .HasOne(b => b.Service) // Booking has a Service
                .WithMany(s => s.Bookings) // A Service can have multiple Bookings
                .HasForeignKey(b => b.ServiceId) // Foreign key in Booking table
                .OnDelete(DeleteBehavior.Restrict); // Define delete behavior

            builder.Entity<Service>()
                .HasOne(s => s.ServiceType)
                .WithMany(st => st.Services)
                .HasForeignKey(s => s.ServiceTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Service>()
                .HasOne(s => s.ServiceCoverage)
                .WithOne(sc => sc.Service)
                .HasForeignKey<ServiceCoverage>(sc => sc.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
