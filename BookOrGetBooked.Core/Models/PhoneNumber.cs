using BookOrGetBooked.Shared.Utilities;

namespace BookOrGetBooked.Core.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string Prefix { get; private set; }
        public string Number { get; private set; }

        // Foreign Key for User
        public int UserId { get; private set; }
        public User User { get; set; } = null!;

        private PhoneNumber(string prefix, string number, int userId)
        {
            Prefix = prefix;
            Number = number;
            UserId = userId;
        }

        public static PhoneNumber Create(string prefix, string number, int userId)
        {
            // Validate parameters
            PhoneNumberValidator.ValidatePrefix(prefix);
            PhoneNumberValidator.ValidateNumber(number);

            if (userId <= 0)
            {
                throw new ArgumentException("UserId must be a positive number.", nameof(userId));
            }

            return new PhoneNumber(prefix, number, userId);
        }

        public void Update(string prefix, string number, int userId)
        {
            // Validate parameters
            PhoneNumberValidator.ValidatePrefix(prefix);
            PhoneNumberValidator.ValidateNumber(number);

            if (userId <= 0)
            {
                throw new ArgumentException("UserId must be a positive number.", nameof(userId));
            }

            Prefix = prefix;
            Number = number;
            UserId = userId;
        }

        public override string ToString()
        {
            return $"{Prefix} {Number}";
        }
    }
}
