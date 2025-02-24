using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Shared.Utilities
{
    public static class PhoneNumberValidator
    {
        public static void ValidatePrefix(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException("Prefix cannot be null or empty.", nameof(prefix));

            if (!prefix.StartsWith('+') || prefix.Skip(1).Any(c => !char.IsDigit(c)))
                throw new ArgumentException("Invalid prefix format.", nameof(prefix));
        }

        public static void ValidateNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Number cannot be null or empty.", nameof(number));

            if (!number.All(c => char.IsDigit(c) || c == ' ' || c == '-'))
                throw new ArgumentException("Invalid phone number format.", nameof(number));
        }
    }
}
