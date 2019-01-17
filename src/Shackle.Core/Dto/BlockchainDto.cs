using System.Collections.Generic;
using System.Linq;
using Shackle.Core.Models;

namespace Shackle.Core.Dto
{
    public class BlockchainDto
    {
        public int Difficulty { get; set; }
        public IEnumerable<BlockDto> Blocks { get; set; }
        public IEnumerable<TransactionDto> PendingTransactions { get; set; }

        public BlockchainDto()
        {
        }

        public BlockchainDto(Blockchain blockchain)
        {
            Difficulty = blockchain.Difficulty;
            Blocks = blockchain.Blocks.Select(b => new BlockDto(b));
            PendingTransactions = blockchain.PendingTransactions.Select(t => new TransactionDto(t));
        }
    }
}