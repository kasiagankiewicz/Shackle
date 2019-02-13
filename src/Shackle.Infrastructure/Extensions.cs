using Microsoft.Extensions.DependencyInjection;
using Shackle.Core.Factories;
using Shackle.Core.Factories.Hash;
using Shackle.Infrastructure.Blockchain;
using Shackle.Infrastructure.Crypto;
using Shackle.Services.Accounts;
using Shackle.Services.Blockchain;
using Shackle.Services.Crypto;

namespace Shackle.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddShackle(this IServiceCollection services)
        {
            services.AddHostedService<BlockchainHostedService>();
            services.AddSingleton<IBlockchainService, BlockchainService>();
            services.AddSingleton<IHashGenerator, HashGenerator>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IHashInputProvider, HashInputProvider>();
            services.AddSingleton<ICryptoFactory, CryptoFactory>();
            services.AddSingleton<ISigner, Signer>();

            return services;
        }
    }
}