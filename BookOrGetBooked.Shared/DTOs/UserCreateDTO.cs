using BookOrGetBooked.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

public class UserCreateDTO
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    public List<PhoneNumberCreateDTO> PhoneNumbers { get; set; } = new List<PhoneNumberCreateDTO>();
}
