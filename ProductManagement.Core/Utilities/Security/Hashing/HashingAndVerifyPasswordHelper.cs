using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductManagement.Core.Utilities.Security.Hashing
{
    public static class HashingAndVerifyPasswordHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordKey)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordKey)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordKey))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (computedHash.Where((t, i) => t != passwordHash[i]).Any())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
