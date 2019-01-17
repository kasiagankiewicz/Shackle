using System.Collections.Generic;

namespace Shackle.Core.Models
{
    public interface IBlockchain
    {
        int Difficulty { get; }
        IEnumerable<Block> Blocks { get; }
        IEnumerable<Transaction> PendingTransactions { get; }
        void SetDifficulty(int difficulty);
        void CreateGenesisBlock();
        void AddTransactions(params Transaction[] transactions);
        void Mine(Miner miner);
    }
}