using System;
using System.Collections.Generic;
using System.Linq;

namespace Shackle.Core.Models
{
    public class BlockData
    {
        public int Index { get; }
        public string PreviousHash { get; }
        public IEnumerable<Transaction> Transactions { get; }
        public DateTime Timestamp { get; }
        public int Nonce { get; private set; }

        private BlockData(int index, string previousHash,
            IEnumerable<Transaction> transactions, DateTime timestamp, int nonce)
        {
            Index = index;
            PreviousHash = previousHash;
            Transactions = new HashSet<Transaction>(transactions);
            Timestamp = timestamp;
            Nonce = nonce;
        }

        public void IncrementNonce()
        {
            Nonce++;
        }

        public static BlockData Genesis(DateTime timestamp, int nonce)
            => new BlockData(0, string.Empty, Enumerable.Empty<Transaction>(), timestamp, nonce);

        public static BlockData Next(Block previousBlock, IEnumerable<Transaction> transactions, DateTime timestamp)
            => new BlockData(previousBlock.Index+1, previousBlock.Hash, transactions, timestamp, 0);
    }
}