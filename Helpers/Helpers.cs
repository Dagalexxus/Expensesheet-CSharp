using System.Security.Cryptography;
using System.Text;

namespace api.Helpers{
    public static class Passwords{
        private static int keySize = 64;
        private static int iterations = 350000;
        private static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        public static string Hashing(string password, out byte[] salt){
            salt = RandomNumberGenerator.GetBytes(keySize);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        public static bool passwordCheck(string password, string passwordHash, byte[] salt){
            byte[] toCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(toCompare, Convert.FromHexString(passwordHash));
        }
    }
}