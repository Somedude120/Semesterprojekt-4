using System;
using System.Security.Cryptography;

namespace AsyncServer.Class
{
    public class SaltedHash
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        public SaltedHash(string password)
        {
            var saltBytes = new byte[32];
            //Denne her randomizer min saltbytes
            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);
            //Output Salt og Hash
            Salt = Convert.ToBase64String(saltBytes);
            Hash = ComputeHash(Salt, password);
        }

        static string ComputeHash(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000))
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        }

        public bool Verify(string salt, string hash, string password)
        {
            return hash == ComputeHash(salt, password);
        }
        //static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        //{
        //    HashAlgorithm algorithm = new SHA256Managed();

        //    byte[] plainTextWithSaltBytes =
        //        new byte[plainText.Length + salt.Length];

        //    for (int i = 0; i < plainText.Length; i++)
        //    {
        //        plainTextWithSaltBytes[i] = plainText[i];
        //    }
        //    for (int i = 0; i < salt.Length; i++)
        //    {
        //        plainTextWithSaltBytes[plainText.Length + i] = salt[i];
        //    }

        //    return algorithm.ComputeHash(plainTextWithSaltBytes);
        //}
        //internal static string GetStringSha256Hash(string text)
        //{
        //    if (String.IsNullOrEmpty(text))
        //        return String.Empty;

        //    using (var sha = new System.Security.Cryptography.SHA256Managed())
        //    {
        //        byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
        //        byte[] hash = sha.ComputeHash(textData);
        //        return BitConverter.ToString(hash).Replace("-", String.Empty);
        //    }
        //}
    }
}