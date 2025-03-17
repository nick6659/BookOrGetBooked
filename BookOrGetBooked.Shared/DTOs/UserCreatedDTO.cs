using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Shared.DTOs;

public class UserCreatedDTO
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public List<PhoneNumberResponseDTO> PhoneNumbers { get; set; } = new List<PhoneNumberResponseDTO>();
}
