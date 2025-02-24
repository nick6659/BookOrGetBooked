using System.Collections.Generic;

namespace BookOrGetBooked.Core.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public int UserTypeId { get; private set; }
        public UserType UserType { get; set; } = null!;
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        private User(string name, string email, int userTypeId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            if (userTypeId < 0)
                throw new ArgumentException("UserTypeId must be at least zero.", nameof(userTypeId));

            Name = name;
            Email = email;
            UserTypeId = userTypeId;
        }

        public static User Create(string name, string email, int userTypeId)
        {
            return new User(name, email, userTypeId);
        }
    }
}
