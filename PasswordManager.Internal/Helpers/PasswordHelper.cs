using System;
using System.Security.Cryptography;
using System.Text;
using PasswordManager.Internal.Contract.Models;

namespace PasswordManager.Internal.Helpers
{
    public static class PasswordHelper
    {
        public static HashResult GeneratePassword(string password)
        {
            var rnd = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rnd.GetBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var md = MD5.Create();
            var saltedPass = password + salt;
            var hash = Convert.ToBase64String(md.ComputeHash(Encoding.Unicode.GetBytes(saltedPass)));

            return new HashResult(hash, salt);
        }

        public static bool ComparePassword(Access access, string password)
        {
            var md = MD5.Create();
            var saltedPass = password + access?.PasswordSalt;
            var hash = Convert.ToBase64String(md.ComputeHash(Encoding.Unicode.GetBytes(saltedPass)));

            return hash == access?.PasswordHash;
        }
    }

    public record HashResult(string Hash, string Salt)
    { }
}
