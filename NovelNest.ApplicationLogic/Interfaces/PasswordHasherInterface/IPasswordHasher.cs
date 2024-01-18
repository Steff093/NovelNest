using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Interfaces.PasswordHasherInterface
{
    public interface IPasswordHasher
    {
        byte[] PBKDF2Hash(string input, byte[] salt);
        byte[] GenerateRandomSalt();
    }
}
