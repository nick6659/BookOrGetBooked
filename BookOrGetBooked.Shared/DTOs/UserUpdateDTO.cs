namespace BookOrGetBooked.Shared.DTOs
{
    public class UserUpdateDTO
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required int UserTypeId { get; set; }
        public ICollection<PhoneNumberResponseDTO> PhoneNumbers { get; set; } = new List<PhoneNumberResponseDTO>();
    }
}
