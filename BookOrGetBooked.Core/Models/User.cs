using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public ICollection<Service> ProvidedServices { get; set; } = new List<Service>();

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
