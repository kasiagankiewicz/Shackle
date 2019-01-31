using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Shackle.Core.Factories.Hash;
using Shackle.Core.Models;

namespace Shackle.Core.Services
{
    public class BlockchainRunner : IBlockchainRunner
    {
        private readonly ISigner _signer;
        private bool _isRunning;
        private readonly ConcurrentQueue<Transaction> _transactions = new ConcurrentQueue<Transaction>();
        private readonly Miner _miner = new Miner("miner1");
        public Blockchain Blockchain { get; }

        public BlockchainRunner(IHashGenerator hashGenerator, IHashInputProvider hashInputProvider,
            IAccountService accountService, ISigner signer)
        {
            _signer = signer;
            Blockchain = new Blockchain(hashGenerator, hashInputProvider);
            accountService.Join("user1");
            accountService.Join("user2");
        }
        
        public Block GetBlock(int index)
            => Blockchain.Blocks.SingleOrDefault(b => b.Index == index);

        public Block GetLastBlock()
            => Blockchain.Blocks.LastOrDefault();

        public async Task StartAsync()
        {
            Console.WriteLine("Starting Blockchain...");
            Blockchain.SetDifficulty(2);
            Blockchain.CreateGenesisBlock();
            Console.WriteLine($"Difficulty: {Blockchain.Difficulty}");

            _isRunning = true;
            while (_isRunning)
            {
                ProcessTransactions();
                Console.WriteLine($"Pending transaction: {Blockchain.PendingTransactions.Count()}");
                Blockchain.Mine(_miner);
                Console.WriteLine(Blockchain.Blocks.Last());
                await Task.Delay(5000);
            }
        }

        private void ProcessTransactions()
        {
            while (_transactions.TryDequeue(out var transaction))
            {
                Blockchain.AddTransactions(transaction);
            }
        }

        public Task StopAsync()
        {
            Console.WriteLine("Stopping Blockchain...");
            _isRunning = false;

            return Task.CompletedTask;
        }

        public void CreateTransfer(Account sender, Account receiver, long amount)
        {
            var data = sender.Address.Bytes.Union(receiver.Address.Bytes);
            var signature = _signer.Sign(data, sender.PrivateKey);
            var transaction = new Transaction(sender, receiver, amount, signature);
            _transactions.Enqueue(transaction);
        }

        public void SetDifficulty(int difficulty)
        {
            Blockchain.SetDifficulty(difficulty);
        }

        public int GetDifficulty()
            => Blockchain.Difficulty;
    }
}
    
