using System.Security.Cryptography;
using System.Text;

namespace BookOrGetBooked.Shared.Utilities
{
    public static class TokenHasher
    {
        public static string Hash(string token)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
