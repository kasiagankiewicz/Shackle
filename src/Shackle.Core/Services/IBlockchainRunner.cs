using System.Threading.Tasks;
using Shackle.Core.Models;

namespace Shackle.Core.Services
{
    public interface IBlockchainRunner
    {
        Blockchain Blockchain { get; }
        Block Get(int index);
        Task StartAsync();
        Task StopAsync();
        void CreateTransfer(Account sender, Account receiver, long amount);
    }
}