using Shackle.Core.Models;

namespace Shackle.Core.Services
{
    public interface IBlockchainService
    {
        IBlockchain Create();
        Transaction CreateTransaction(Account sender, Account receiver, long amount);
    }
}