using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Shackle.Core.Factories.Hash;

namespace Shackle.Core.Models
{
    public class Blockchain : IBlockchain
    {
        private readonly ISet<Block> _blocks = new HashSet<Block>();
        private readonly ConcurrentQueue<Transaction> _pendingTransactions = new ConcurrentQueue<Transaction>();
        private int _difficulty = 0;
        private readonly IHashGenerator _hashGenerator;
        private readonly IHashInputProvider _hashInputProvider;
        private readonly Stopwatch _watch = new Stopwatch();

        public int Difficulty => _difficulty;
        public IEnumerable<Block> Blocks => _blocks;
        public IEnumerable<Transaction> PendingTransactions => _pendingTransactions.ToArray();

        public Blockchain(IHashGenerator hashGenerator, IHashInputProvider hashInputProvider)
        {
            _hashGenerator = hashGenerator;
            _hashInputProvider = hashInputProvider;
        }

        public void SetDifficulty(int difficulty)
        {
            if (_difficulty < 0)
            {
                throw new ArgumentException($"Invalid difficulty {difficulty}",
                    nameof(difficulty));
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
            CreateNextBlock(blockData, GetHash(blockData), 0,string.Empty);
        }

        public void AddTransactions(params Transaction[] transactions)
        {
            if (transactions == null || !transactions.Any())
            {
                Console.WriteLine("No pending transactions.");

                return;
            }

            foreach (var transaction in transactions)
            {
                _pendingTransactions.Enqueue(transaction);
            }

            Console.WriteLine($"Added {transactions.Length} pending transactions.");
        }

        public void Mine(Miner miner)
        {
            if (!_blocks.Any())
            {
                throw new Exception("Genesis block was not created.");
            }

            var previousBlock = _blocks.Last();
            var blockData = BlockData.Next(previousBlock, PendingTransactions, DateTime.UtcNow);
            var (hash, miningTime) = Mine(blockData, miner);
            CreateNextBlock(blockData, hash, miningTime, previousBlock.Hash);
            ProcessPendingTransactions(miner);
        }

        private (string hash, long miningTime) Mine(BlockData blockData, Miner miner)
        {
            _watch.Restart();
            _watch.Start();
            var difficultyString = GetDifficultyString();
            Console.WriteLine($"\tBlock: {blockData.Index} - difficulty string: {difficultyString}");
            var hash = GetHash(blockData);
            while (hash.Substring(0, Difficulty) != difficultyString)
            {
                hash = GetHash(blockData);
                blockData.IncrementNonce();
            }
            _watch.Stop();

            Console.WriteLine($"\tBlock: {blockData.Index} - nonce: {blockData.Nonce} " +
                              $"was mined by: {miner.Name} " + 
                              $"Completed in {_watch.ElapsedMilliseconds} ms");

            return (hash, _watch.ElapsedMilliseconds);
        }

        private void ProcessPendingTransactions(Miner miner)
        {
            while (_pendingTransactions.TryDequeue(out _))
            {
            }
        }

        private string GetDifficultyString()
            => Difficulty == 0
                ? string.Empty
                : Enumerable.Range(0, Difficulty)
                    .Select(_ => "0")
                    .Aggregate((a, b) => $"{a}{b}");

        private void CreateNextBlock(BlockData blockData, string hash, long miningTime, string previousHash)
        {
            var block = Block.Create(blockData.Index, previousHash, hash,
                blockData.Transactions, blockData.Timestamp, blockData.Nonce, miningTime);
            _blocks.Add(block);
        }

        private string GetHash(BlockData blockData)
            => _hashGenerator.Generate(_hashInputProvider.Create(blockData));

        private static void ValidateData(string data)
        {
            if (string.IsNullOrWhiteSpace(data) || data.Length > 1000)
            {
                throw new ArgumentException("Invalid data.", nameof(data));
            }
        }
    }
}