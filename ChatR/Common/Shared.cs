using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ChatR.Common
{
    public static class Shared
    {
        public static int port = 55555;

        public static string ReadLine(StreamReader sr)
        {
            var message = sr.ReadLine();
            if (message == null)
            {
                throw new Exception("! :: ERR0R READING FR0M STREAM :: !");
            }
            return message;
        }

        public static string EncodeURL(string url)
        {
            // Taken From: http://stackoverflow.com/a/11477466
            // byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(url);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
                // without dashes
               .Replace("/", "_")
                // make lowercase
               .ToLower();

            return encoded;
        }

        public static string CreateRandomName(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}

