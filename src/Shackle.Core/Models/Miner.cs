using System;

namespace Shackle.Core.Models
{
    public class Miner
    {
        public string Name { get; }
        public long Amount { get; private set; }

        public Miner(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Miner name cannot be null.");
            }

            Name = name;
            Amount = 0;
        }

        public void Reward(long amount)
        {
            Amount += amount;
        }

        public override string ToString()
            => $"Miner: '{Name}' - amount: {Amount}";
    }
}