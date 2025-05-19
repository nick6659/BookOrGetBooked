namespace BookOrGetBooked.Shared.Validation
{
    public static class EmailValidator
    {
        private const string Pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public static bool IsValid(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   System.Text.RegularExpressions.Regex.IsMatch(email, Pattern);
        }

        public const string ValidationErrorMessage = "Invalid email format. Must include '@' and a domain like '.com' or '.dk'.";
    }
}
