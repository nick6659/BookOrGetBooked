using BookOrGetBooked.Infrastructure.Data;
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

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; } = null!;

        public int CurrencyId { get; private set; }
        public Currency Currency { get; private set; } = null!;

        public string ProviderId { get; private set; } = null!;
        public ApplicationUser Provider { get; private set; } = null!;

        public bool IsDeleted { get; private set; } // For soft delete
        public DateTime? DeletedAt { get; private set; }
        public bool IsInactive { get; private set; } // For temporarily disabling the service

        public ICollection<Booking> Bookings { get; private set; } = [];
        public ServiceCoverage? ServiceCoverage { get; set; }

        private Service(string name, string? description, int serviceTypeId, decimal price, int currencyId, string providerId)
        {
            Name = name;
            Description = description;
            ServiceTypeId = serviceTypeId;
            Price = price;
            CurrencyId = currencyId;
            ProviderId = providerId;
            IsDeleted = false;
            IsInactive = false;
        }

        public static Service Create(string name, string? description, int serviceTypeId, decimal price, int currencyId, string providerId, ServiceCoverage? serviceCoverage = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            if (serviceTypeId <= 0)
                throw new ArgumentException("ServiceTypeId must be a positive number.", nameof(currencyId));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            if (currencyId <= 0)
                throw new ArgumentException("CurrencyId must be a positive number.", nameof(currencyId));

            if (string.IsNullOrWhiteSpace(providerId))
                throw new ArgumentException("ProviderId must be a valid user ID.", nameof(providerId));

            return new Service(name, description, serviceTypeId, price, currencyId, providerId)
            {
                ServiceCoverage = serviceCoverage
            };
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
