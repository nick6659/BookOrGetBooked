using System.ComponentModel.DataAnnotations;

public class PhoneNumberCreateDTO
{
    [Required(ErrorMessage = "Phone number prefix is required.")]
    public string Prefix { get; set; } = string.Empty; // Country code, e.g., +1

    [Required(ErrorMessage = "Phone number is required.")]
    public string Number { get; set; } = string.Empty; // Phone number
}
