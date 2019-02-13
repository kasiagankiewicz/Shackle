using Shackle.Core.Models;

namespace Shackle.Core.Factories
{
    public class BlockchainFactory : IBlockchainFactory
    {
        private readonly IHashFactory _hashFactory;

        public BlockchainFactory(IHashFactory hashFactory)
        {
            _hashFactory = hashFactory;
        }
        
        public IBlockchain Create(int difficulty = 2)
        {
            var blockchain = new Blockchain(_hashFactory);
            blockchain.CreateGenesisBlock();
            blockchain.SetDifficulty(difficulty);

            return blockchain;
        }
    }
}