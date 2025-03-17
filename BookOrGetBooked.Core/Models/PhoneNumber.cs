using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.Utilities;

public class PhoneNumber
{
    public int Id { get; private set; }
    public string Prefix { get; private set; }
    public string Number { get; private set; }

    // Foreign Key for User (Automatically Handled by EF)
    public int UserId { get; private set; }
    public User User { get; set; } = null!;

    private PhoneNumber(string prefix, string number)
    {
        Prefix = prefix;
        Number = number;
    }

    public static PhoneNumber Create(string prefix, string number)
    {
        PhoneNumberValidator.ValidatePrefix(prefix);
        PhoneNumberValidator.ValidateNumber(number);

        return new PhoneNumber(prefix, number);
    }

    public void Update(string prefix, string number)
    {
        PhoneNumberValidator.ValidatePrefix(prefix);
        PhoneNumberValidator.ValidateNumber(number);

        Prefix = prefix;
        Number = number;
    }

    public override string ToString()
    {
        return $"{Prefix} {Number}";
    }
}
