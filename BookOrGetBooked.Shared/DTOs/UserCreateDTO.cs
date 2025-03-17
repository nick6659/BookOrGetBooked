using BookOrGetBooked.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

public class UserCreateDTO
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    public required PhoneNumberCreateDTO PhoneNumber { get; set; }
}
