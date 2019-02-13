using System.Collections.Generic;
using Shackle.Core.Models;

namespace Shackle.Services.Crypto
{
    public interface ISigner
    {
        Signature Sign(IEnumerable<byte> data, PrivateKey privateKey);
    }
}