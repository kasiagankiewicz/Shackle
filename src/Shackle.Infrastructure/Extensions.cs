using Microsoft.Extensions.DependencyInjection;
using Shackle.Core.Factories;
using Shackle.Core.Repositories;
using Shackle.Core.Services;
using Shackle.Infrastructure.Blockchain;
using Shackle.Infrastructure.Crypto;
using Shackle.Infrastructure.Factories;
using Shackle.Infrastructure.Persistence.InMemory;
using Shackle.Services.Accounts;
using Shackle.Services.Blockchain;
using IBlockchainService = Shackle.Services.Blockchain.IBlockchainService;

namespace Shackle.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddShackle(this IServiceCollection services)
        {
            services.AddSingleton<IBlockchainFactory, BlockchainFactory>();
            services.AddSingleton<ICryptoFactory, CryptoFactory>();
            services.AddSingleton<IHashFactory, HashFactory>();
            services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
            services.AddSingleton<ISigner, Signer>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IBlockchainService, BlockchainService>();
            services.AddHostedService<BlockchainHostedService>();
            
            return services;
        }
    }
}