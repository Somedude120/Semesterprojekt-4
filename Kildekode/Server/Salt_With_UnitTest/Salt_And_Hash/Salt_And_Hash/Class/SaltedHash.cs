using System;
using System.Security.Cryptography;

namespace Salt_And_Hash
{
    public class SaltedHash
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        public string MakeSalt()
        {
            var saltBytes = new byte[32];

            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);
            string salt = Convert.ToBase64String(saltBytes);

            return salt;
        }

        public string ComputeHash(string salt, string password)
        {
            string saltyHashPwd = salt + password;
            saltyHashPwd = GetStringSha256Hash(saltyHashPwd);

            return saltyHashPwd;
        }

        public string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
}