using Microsoft.EntityFrameworkCore;
using BookOrGetBooked.Core.Models;

namespace BookOrGetBooked.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Booking and User (Booker)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Booker) // Booker navigation property
                .WithMany()            // Booker does not have a collection of bookings
                .HasForeignKey(b => b.BookerId) // Foreign key in Booking table
                .OnDelete(DeleteBehavior.Restrict); // Define delete behavior

            // Configure the relationship between Booking and User (Provider)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Provider) // Provider navigation property
                .WithMany()              // Provider does not have a collection of bookings
                .HasForeignKey(b => b.ProviderId) // Foreign key in Booking table
                .OnDelete(DeleteBehavior.Restrict); // Define delete behavior
        }
    }
}
