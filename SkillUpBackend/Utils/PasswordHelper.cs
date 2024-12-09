using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace SkillUpBackend.Utils
{
    public class PasswordHelper
    {
        public bool IsValidPassword(string inputPassword, string storedPassword)
        {
            // Split the stored password into salt and hash
            var parts = storedPassword.Split(':');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = parts[1];

            // Hash the input password with the same salt
            string inputHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: inputPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            // Compare the hashes
            return hash == inputHash;
        }

        public string HashPassword(string password)
        {
            // Generate a salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            // Combine the salt and hash for storage
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }
    }
}
