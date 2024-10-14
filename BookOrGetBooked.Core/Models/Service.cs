using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Models
{
    public class Service
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }

        public int CurrencyId { get; set; }
        public required Currency Currency { get; set; }

        public int ProviderId { get; set; }
        public required User Provider { get; set; }

        public required ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
