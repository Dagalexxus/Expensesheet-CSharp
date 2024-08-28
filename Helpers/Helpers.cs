using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using api.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;

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

        public static string createToken(User user, IConfiguration configuration){

#pragma warning disable CS8604 // Possible null reference argument.
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("Password:Tokensecret").Value));
#pragma warning restore CS8604 // Possible null reference argument.

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.SigningCredentials = credentials;
            tokenDescriptor.Expires = DateTime.Now.AddHours(2);
            tokenDescriptor.Claims = new Dictionary<string, object> {
                {"name", user.username},
                {"sub", user.id},
                {"email", user.userEmail},
                {"role", "user"}
            };
            tokenDescriptor.Issuer = configuration.GetSection("Password:Issuer").Value;
            tokenDescriptor.Audience = configuration.GetSection("Password:Audience").Value;

            
            return new JsonWebTokenHandler().CreateToken(tokenDescriptor); 
        }
  }
}