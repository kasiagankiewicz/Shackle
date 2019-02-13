using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Shackle.Core.Factories;

namespace Shackle.Core.Models
{
    public class Blockchain : IBlockchain
    {
        private readonly ISet<Block> _blocks = new HashSet<Block>();
        private readonly ConcurrentQueue<Transaction> _pendingTransactions = new ConcurrentQueue<Transaction>();
        private int _difficulty = 0;
        private readonly IHashFactory _hashFactory;

        public int Difficulty => _difficulty;
        public Block CurrentBlock => _blocks.LastOrDefault();
        public IEnumerable<Block> Blocks => _blocks;
        public IEnumerable<Transaction> PendingTransactions => _pendingTransactions.ToArray();

        public Blockchain(IHashFactory hashFactory)
        {
            _hashFactory = hashFactory;
        }

        public void SetDifficulty(int difficulty)
        {
            if (_difficulty < 0)
            {
                throw new ArgumentException($"Invalid difficulty {difficulty}", nameof(difficulty));
            }

            _difficulty = difficulty;
        }

        public void CreateGenesisBlock()
        {
            if (_blocks.Any())
            {
                throw new Exception("Genesis block was already created.");
            }

            var blockData = BlockData.Genesis(DateTime.UtcNow, 0);
            CreateNextBlock(blockData, GetHash(blockData), string.Empty);
        }

        public void AddTransactions(params Transaction[] transactions)
        {
            if (transactions == null || !transactions.Any())
            {
                return;
            }

            foreach (var transaction in transactions)
            {
                _pendingTransactions.Enqueue(transaction);
            }
        }

        public Block Mine(Miner miner)
        {
            if (!_blocks.Any())
            {
                throw new Exception("Genesis block was not created.");
            }

            var previousBlock = _blocks.Last();
            var blockData = BlockData.Next(previousBlock, PendingTransactions, DateTime.UtcNow);
            var hash = Mine(blockData, miner);
            var block = CreateNextBlock(blockData, hash, previousBlock.Hash);
            ProcessPendingTransactions();

            return block;
        }

        private string Mine(BlockData blockData, Miner miner)
        {
            Console.WriteLine($"\tBlock: {blockData.Index} - difficulty string: {DifficultyString}");
            var hash = GetHash(blockData);
            while (hash.Substring(0, Difficulty) != DifficultyString)
            {
                hash = GetHash(blockData);
                blockData.IncrementNonce();
            }

            return hash;
        }

        private void ProcessPendingTransactions()
        {
            while (_pendingTransactions.TryDequeue(out _))
            {
            }
        }

        private Block CreateNextBlock(BlockData blockData, string hash, string previousHash)
        {
            var block = Block.Create(blockData.Index, previousHash, hash,
                blockData.Transactions, blockData.Timestamp, blockData.Nonce);
            _blocks.Add(block);

            return block;
        }

        private string GetHash(BlockData blockData) => _hashFactory.Create(blockData.GetHashData());

        private string DifficultyString
            => Difficulty == 0
                ? string.Empty
                : Enumerable.Range(0, Difficulty)
                    .Select(_ => "0")
                    .Aggregate((a, b) => $"{a}{b}");
    }
}