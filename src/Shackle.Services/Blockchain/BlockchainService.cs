using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shackle.Core.Factories;
using Shackle.Core.Models;
using Shackle.Core.Repositories;
using Shackle.Core.Services;
using Shackle.Services.Dto;

namespace Shackle.Services.Blockchain
{
    public class BlockchainService : IBlockchainService
    {
        private IBlockchain _blockchain;
        private readonly IBlockchainFactory _blockchainFactory;
        private readonly ITransactionService _transactionService;
        private readonly IAccountRepository _accountRepository;
        private readonly ISigner _signer;
        private readonly ILogger<BlockchainService> _logger;
        private bool _isRunning;
        private readonly Miner _miner = new Miner("miner1");
        private readonly Stopwatch _watch = new Stopwatch();

        public BlockchainService(IBlockchainFactory blockchainFactory,
            ITransactionService transactionService,
            IAccountRepository accountRepository,
            ISigner signer,
            ILogger<BlockchainService> logger)
        {
            _blockchainFactory = blockchainFactory;
            _transactionService = transactionService;
            _accountRepository = accountRepository;
            _signer = signer;
            _logger = logger;
        }


        public BlockchainDto GetBlockchain() => _blockchain is null ? null : new BlockchainDto(_blockchain);

        public BlockDto GetBlock(int index)
            => MapBlockDto(() => _blockchain.Blocks.SingleOrDefault(b => b.Index == index));

        public BlockDto GetCurrentBlock()
            => MapBlockDto(() => _blockchain.CurrentBlock);

        private BlockDto MapBlockDto(Func<Block> blockAccessor)
        {
            var block = blockAccessor();

            return block is null ? null : new BlockDto(block);
        }

        public async Task StartAsync()
        {
            _blockchain = _blockchainFactory.Create();
            _logger.LogInformation("Starting Blockchain...");
            _logger.LogInformation($"Difficulty: {_blockchain.Difficulty}");

            _isRunning = true;
            while (_isRunning)
            {
                _logger.LogInformation($"Pending transactions: {_blockchain.PendingTransactions.Count()}");
                _watch.Restart();
                _watch.Start();
                var difficulty = GetDifficultyString(_blockchain.Difficulty);
                var blockNumber = _blockchain.Blocks.Count() + 1;
                _logger.LogInformation($"Mining block #{blockNumber} difficulty '{difficulty}'");
                var minedBlock = _blockchain.Mine(_miner);
                _watch.Stop();
                var miningTime = _watch.ElapsedMilliseconds;
                minedBlock.SetMiningTime(miningTime);
                _logger.LogInformation($"Block #{blockNumber} nonce: {minedBlock.Nonce} " +
                                       $"was mined by: {_miner.Name} " +
                                       $"Completed in {miningTime} ms");
                _logger.LogInformation(minedBlock.ToString());
                await Task.Delay(5000);
            }
        }

        public Task StopAsync()
        {
            _logger.LogInformation("Stopping Blockchain...");
            _isRunning = false;

            return Task.CompletedTask;
        }

        public void CreateTransaction(string sender, string receiver, long amount)
        {
            var senderAccount = _accountRepository.Get(sender);
            var receiverAccount = _accountRepository.Get(receiver);
            var transaction = _transactionService.Execute(senderAccount, receiverAccount, amount, _signer);
            _blockchain.AddTransactions(transaction);
            _logger.LogInformation($"Executed transaction from '{sender}' to '{receiver}' amount {amount}.");
        }

        public void SetDifficulty(int difficulty)
        {
            _blockchain.SetDifficulty(difficulty);
        }

        public int GetDifficulty() => _blockchain.Difficulty;

        private static string GetDifficultyString(int difficulty)
            => difficulty == 0
                ? string.Empty
                : Enumerable.Range(0, difficulty)
                    .Select(_ => "0")
                    .Aggregate((a, b) => $"{a}{b}");
    }
}
    
