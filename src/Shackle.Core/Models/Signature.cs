using System;
using System.Linq;

namespace Shackle.Core.Models
{
    public class Signature
    {
        public byte[] Bytes { get; set; }

        public Signature(byte[] bytes)
        {
            Bytes = bytes ?? throw new ArgumentNullException(nameof(bytes), "Bytes can not be null.");
        }
        
        public override string ToString()
            => $"{string.Join(string.Empty, Bytes.Select(b => b.ToString("X2"))).ToLowerInvariant()}";
    }
}
