using System.Linq;

namespace Shackle.Core.Models
{
    public static class Extensions
    {
        public static string GetStringFromHash(this byte[] hash)
            => hash.Select(b => $"{b:X2}")
                .Aggregate((a, b) => $"{a}{b}")
                .ToLowerInvariant();
    }
}