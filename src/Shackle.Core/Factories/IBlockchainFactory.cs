using Shackle.Core.Models;

namespace Shackle.Core.Factories
{
    public interface IBlockchainFactory
    {
        IBlockchain Create(int difficulty = 2);
    }
}