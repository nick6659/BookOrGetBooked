using System.ComponentModel.DataAnnotations;

namespace BookOrGetBooked.Shared.DTOs
{
    public class ServiceTypeUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
