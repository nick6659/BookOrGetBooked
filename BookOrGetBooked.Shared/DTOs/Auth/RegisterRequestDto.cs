using BookOrGetBooked.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookOrGetBooked.Shared.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public required string Email { get; set; }

        [Required]
        [StrongPassword]
        public required string Password { get; set; }

        [Required]
        [RegularExpression(@"^\+\d{6,15}$", ErrorMessage = "Phone number must be in international format, e.g., +4512345678.")]
        public required string PhoneNumber { get; set; }
    }
}
