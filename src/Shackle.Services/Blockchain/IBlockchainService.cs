using System.Threading.Tasks;
using Shackle.Services.Dto;

namespace Shackle.Services.Blockchain
{
    public interface IBlockchainService
    {
        BlockchainDto GetBlockchain();
        BlockDto GetBlock(int index);
        BlockDto GetCurrentBlock();
        Task StartAsync();
        Task StopAsync();
        void CreateTransaction(string sender, string receiver, long amount);
        void SetDifficulty(int difficulty);
        int GetDifficulty();
    }
}