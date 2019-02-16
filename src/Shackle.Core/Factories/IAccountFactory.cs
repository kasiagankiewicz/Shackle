using Shackle.Core.Models;

namespace Shackle.Core.Factories
{
    public interface IAccountFactory
    {
//        (PrivateKey PrivateKey, PublicKey PublicKey, Address Address) Create(bool compress = true);
        Account Create(string name, long balance);
    }
}