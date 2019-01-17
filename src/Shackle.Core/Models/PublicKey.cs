using System;
using System.Linq;

namespace Shackle.Core.Models
{
    public class PublicKey
    {
        public byte[] Bytes { get; }

        public PublicKey(byte[] bytes, bool compress)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes), "Public key can not be null.");
            }

            if (compress && bytes.Length != 32)
            {
                throw new ArgumentException("Public key must contain exactly 32 bytes.", nameof(bytes));
            }

            if (!compress && bytes.Length != 64)
            {
                throw new ArgumentException("Public key must contain exactly 64 bytes.", nameof(bytes));
            }

            Bytes = bytes;
        }

        public override string ToString()
            => $"{string.Join(string.Empty, Bytes.Select(b => b.ToString("X2")))}";
    }
}