using System;
using System.Collections.Generic;

namespace BookOrGetBooked.Core.Models
{
    public class Service
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public decimal Price { get; private set; }

        public int CurrencyId { get; private set; }
        public Currency Currency { get; private set; } = null!;

        public int ProviderId { get; private set; }
        public User Provider { get; private set; } = null!;

        public bool IsDeleted { get; private set; } // For soft delete
        public DateTime? DeletedAt { get; private set; }
        public bool IsInactive { get; private set; } // For temporarily disabling the service

        public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();

        private Service(string name, string? description, decimal price, int currencyId, int providerId)
        {
            Name = name;
            Description = description;
            Price = price;
            CurrencyId = currencyId;
            ProviderId = providerId;
            IsDeleted = false;
            IsInactive = false;
        }

        public static Service Create(string name, string? description, decimal price, int currencyId, int providerId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            if (currencyId <= 0)
                throw new ArgumentException("CurrencyId must be a positive number.", nameof(currencyId));

            if (providerId <= 0)
                throw new ArgumentException("ProviderId must be a positive number.", nameof(providerId));

            return new Service(name, description, price, currencyId, providerId);
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletedAt = null;
        }

        public void SetInactive(bool inactive)
        {
            IsInactive = inactive;
        }
    }
}
