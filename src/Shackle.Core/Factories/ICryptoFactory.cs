using Shackle.Core.Models;

namespace Shackle.Core.Factories
{
    public interface ICryptoFactory
    {
        (PrivateKey PrivateKey, PublicKey PublicKey, Address Address) Create(bool compress = true);
    }
}