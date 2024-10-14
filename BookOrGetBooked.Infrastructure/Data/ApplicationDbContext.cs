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

    }
}
