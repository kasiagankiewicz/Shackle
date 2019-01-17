using Newtonsoft.Json;
using Shackle.Core.Models;

namespace Shackle.Core.Factories.Hash
{
    public class HashInputProvider : IHashInputProvider
    {
        public string Create(BlockData block)
            => $"{block.Index}{block.PreviousHash}{JsonConvert.SerializeObject(block.Transactions)}" +
               $"{block.Timestamp.Ticks}{block.Nonce}";
    }
}