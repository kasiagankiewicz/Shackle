using System.Security.Cryptography;
using System.Text;

namespace Shackle.Core.Factories
{
    public class HashFactory : IHashFactory
    {
        public string Create(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(bytes);

                return GetStringFromHash(hashBytes);
            }
        }

        private static string GetStringFromHash(byte[] hash)
        {
            var builder = new StringBuilder();
            foreach (var part in hash)
            {
                builder.Append(part.ToString("X2"));
            }
            
            return builder.ToString().ToLowerInvariant();
        }
    }
}