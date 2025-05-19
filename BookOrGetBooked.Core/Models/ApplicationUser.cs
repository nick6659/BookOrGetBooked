using BookOrGetBooked.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace BookOrGetBooked.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
