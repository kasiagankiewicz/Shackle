using Shackle.Core.Models;

namespace Shackle.Core.Services
{
    public interface ITransactionService
    {
        Transaction Execute(Account sender, Account receiver, long amount, ISigner signer);
    }
}