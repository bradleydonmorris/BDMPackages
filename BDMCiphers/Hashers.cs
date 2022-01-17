using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BDMCiphers
{
    public class Hashers
    {
        public static String HashString(String value)
        {
            return Convert.ToBase64String
            (
                SHA256.Create().ComputeHash(
                    Encoding.UTF8.GetBytes(value)
                )
            );
        }

        public static String CreateApiKey()
        {
            String alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var bytes = new byte[256 / 8];
            using (var random = RandomNumberGenerator.Create())
                random.GetBytes(bytes);
            BigInteger dividend = new BigInteger(bytes);
            StringBuilder stringBuilder = new();
            while (dividend != 0)
            {
                dividend = BigInteger.DivRem(dividend, alphabet.Length, out BigInteger remainder);
                stringBuilder.Insert(0, alphabet[Math.Abs(((int)remainder))]);
            }
            return stringBuilder.ToString();
        }
    }
}
