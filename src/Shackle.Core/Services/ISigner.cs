using System.Collections.Generic;
using Shackle.Core.Models;

namespace Shackle.Core.Services
{
    public interface ISigner
    {
        Signature Sign(IEnumerable<byte> data, PrivateKey privateKey);
    }
}