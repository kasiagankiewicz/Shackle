using System;
using System.Linq;

namespace Shackle.Core.Models
{
    public class PrivateKey
    {
        public byte[] Bytes { get; }

        public PrivateKey(byte[] bytes)
        {
            Bytes = bytes ?? throw new ArgumentNullException(nameof(bytes), "Private key can not be null.");
        }

        public override string ToString()
            => $"{string.Join(string.Empty, Bytes.Select(b => b.ToString("X2")))}";
    }
}