namespace BookOrGetBooked.Shared.DTOs
{
    public class UserCreateDTO
    {
        public required string Name { get; set; } = string.Empty; // Required user name
        public required string Email { get; set; } = string.Empty; // Required user email
        public required int UserTypeId { get; set; } // Foreign Key for UserType

        // Collection of phone numbers to associate with the user
        public List<PhoneNumberCreateDTO> PhoneNumbers { get; set; } = new List<PhoneNumberCreateDTO>();
    }
}
