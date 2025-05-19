namespace BookOrGetBooked.Shared.Validation
{
    public static class PhoneNumberValidator
    {
        private const string Pattern = @"^\+\d{6,15}$";

        public static bool IsValid(string phoneNumber)
        {
            return !string.IsNullOrWhiteSpace(phoneNumber)
                && System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, Pattern);
        }

        public const string ValidationErrorMessage = "Phone number must be in international format, e.g., +4512345678.";
    }
}
