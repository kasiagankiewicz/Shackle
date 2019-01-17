using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Shackle.Core.Services;

namespace Shackle.Host.Framework
{
    public class BlockchainHostedService : BackgroundService
    {
        private readonly IBlockchainRunner _blockchainRunner;

        public BlockchainHostedService(IBlockchainRunner blockchainRunner)
        {
            _blockchainRunner = blockchainRunner;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _blockchainRunner.StartAsync();
        }
    }
}