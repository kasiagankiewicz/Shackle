using System.Threading.Tasks;
using Shackle.Core.Models;

namespace Shackle.Services.Blockchain
{
    public interface IBlockchainService
    {
        Core.Models.Blockchain Blockchain { get; }
        Block GetBlock(int index);
        Block GetLastBlock();
        Task StartAsync();
        Task StopAsync();
        void CreateTransaction(string sender, string receiver, long amount);
        void SetDifficulty(int difficulty);
        int GetDifficulty();
    }
}