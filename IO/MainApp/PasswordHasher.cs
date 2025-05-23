using System.Security.Cryptography;
using System.Text;


namespace IO.MainApp
{
    public static class PasswordHasher
    {
        private const string salt = "yoursecretsalt";
        public static string? HashPassword(string password)
        {
            if (password == null) return null;

            using (var sha256 = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(password.Trim() + salt);
                var hashBytes = sha256.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            
            return HashPassword(inputPassword) == hashedPassword.Trim();
        }
    }
}
