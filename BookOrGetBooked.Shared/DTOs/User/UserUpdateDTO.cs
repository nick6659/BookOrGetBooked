using BookOrGetBooked.Shared.DTOs.PhoneNumber;

public class UserUpdateDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }

    // Allow multiple phone numbers later
    public ICollection<PhoneNumberResponseDTO> PhoneNumbers { get; set; } = new List<PhoneNumberResponseDTO>();
}
