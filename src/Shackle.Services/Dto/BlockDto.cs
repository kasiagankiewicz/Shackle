using System;
using System.Collections.Generic;
using System.Linq;
using Shackle.Core.Models;

namespace Shackle.Services.Dto
{
    public class BlockDto
    {
        public int Index { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public IEnumerable<TransactionDto> Transactions { get; set; }
        public DateTime Timestamp { get; set; }
        public int Nonce { get; set; }
        public long MiningTime { get; set; }

        public BlockDto()
        {
        }

        public BlockDto(Block block)
        {
            Index = block.Index;
            PreviousHash = block.PreviousHash;
            Hash = block.Hash;
            Transactions = block.Transactions.Select(t => new TransactionDto(t));
            Timestamp = block.Timestamp;
            Nonce = block.Nonce;
            MiningTime = block.MiningTime;
        }
    }
}