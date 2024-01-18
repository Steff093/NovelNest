using Microsoft.EntityFrameworkCore.Query.Internal;
using NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface;
using System.Diagnostics;
using System.Security.Cryptography;

namespace NovelNest.ApplicationLogic.Common.PasswordHash
{
    public class PasswordHasher : IPasswordHasher
    {
        public byte[] PBKDF2Hash(string input, byte[] salt)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, iterations: 5000))
            {
                return pbkdf2.GetBytes(20); 
            }
        }

        public byte[] GenerateRandomSalt()
        {
            Debug.WriteLine("GenerateRandomSalt wird aufgerufen!");
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[32];
                rng.GetBytes(salt);
                return salt;
            }
        }
    }
}
