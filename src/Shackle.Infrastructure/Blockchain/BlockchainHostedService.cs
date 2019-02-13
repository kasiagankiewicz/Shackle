using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Shackle.Services.Blockchain;

namespace Shackle.Infrastructure.Blockchain
{
    public class BlockchainHostedService : BackgroundService
    {
        private readonly IBlockchainService _blockchainService;

        public BlockchainHostedService(IBlockchainService blockchainService)
        {
            _blockchainService = blockchainService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _blockchainService.StartAsync();
        }
    }
}