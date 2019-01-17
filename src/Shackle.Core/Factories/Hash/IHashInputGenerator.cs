using Shackle.Core.Models;

namespace Shackle.Core.Factories.Hash
{
    public interface IHashInputProvider
    {
        string Create(BlockData block);
    }
}