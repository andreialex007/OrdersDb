using System;
using System.Security.Cryptography;
using System.Text;

namespace OrdersDb.Domain.Services.Accounts.User
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            var provider = new MD5CryptoServiceProvider();
            byte[] hash = provider.ComputeHash(Encoding.Default.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            string hashOfInput = HashPassword(password);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return 0 == comparer.Compare(hashOfInput, hash);
        }
    }
}