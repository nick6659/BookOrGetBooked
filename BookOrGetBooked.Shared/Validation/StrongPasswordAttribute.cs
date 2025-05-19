using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookOrGetBooked.Shared.Validation
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var password = value as string;
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        public override string FormatErrorMessage(string name) =>
            "Password must be at least 8 characters long, include an uppercase letter, a digit, and a symbol.";
    }
}
