using System;
using System.Linq;

namespace Shackle.Core.Models
{
    public class Address
    {
        public byte[] Bytes { get; }

        public Address(byte[] bytes)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes), "Address can not be null.");
            }

            if (bytes.Length != 20)
            {
                throw new ArgumentException("Address must contain exactly 20 bytes.", nameof(bytes));
            }

            Bytes = bytes;
        }

        public override string ToString()
            => $"{string.Join(string.Empty, Bytes.Select(b => b.ToString("X2")))}";
    }
}