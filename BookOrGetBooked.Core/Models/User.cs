using System.Collections.Generic;

namespace BookOrGetBooked.Core.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        private User(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Name cannot be null or empty.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Name cannot be null or empty.", nameof(lastName));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public static User Create(string firstName, string lastName, string email)
        {
            return new User(firstName, lastName, email);
        }
    }
}
