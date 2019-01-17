using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shackle.Core.Models
{
    public class Block
    {
        public int Index { get; }
        public string PreviousHash { get; }
        public string Hash { get; }
        public IEnumerable<Transaction> Transactions { get; }
        public DateTime Timestamp { get; }
        public int Nonce { get; }

        private Block(int index, string previousHash, string hash,
            IEnumerable<Transaction> transactions, DateTime timestamp, int nonce)
        {
            Index = index;
            PreviousHash = previousHash;
            Hash = hash;
            Transactions = transactions;
            Timestamp = timestamp;
            Nonce = nonce;
        }

        public override string ToString()
            => $"Block: {Index}{Environment.NewLine}Hash: {Hash}{Environment.NewLine}Timestamp: {Timestamp}" +
               $"{Environment.NewLine}Transactions:{Environment.NewLine}" +
               $"{JsonConvert.SerializeObject(Transactions, Formatting.Indented)}";

        public static Block Create(int index, string previousHash, string hash,
            IEnumerable<Transaction> transactions, DateTime timestamp, int nonce)
            => new Block(index, previousHash, hash, transactions, timestamp, nonce);
    }
}