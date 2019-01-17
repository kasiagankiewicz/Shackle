using System;
using System.Threading;

namespace Shackle.Core.Models
{
    public class Account : IEquatable<Account>
    {
        private long _balance;
        public string Name { get; }
        public PrivateKey PrivateKey { get; }
        public PublicKey PublicKey { get; }
        public Address Address { get; }
        public long Balance => _balance;

        public Account(string name, PrivateKey privateKey, PublicKey publicKey, Address address, long balance = 100)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name can not be empty.", nameof(name));
            }

            if (balance < 0)
            {
                throw new ArgumentException("Balance can not be lower than 0.", nameof(balance));
            }

            Name = name;
            PrivateKey = privateKey ??
                         throw new ArgumentNullException(nameof(privateKey), "Private key can not be null.");
            PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey), "Public key can not be null.");
            Address = address ?? throw new ArgumentNullException(nameof(address), "Address can not be null.");
            _balance = balance;
        }

        public void IncreaseAccountBalance(long amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount can not be lower than 0.", nameof(amount));
            }

            Interlocked.Add(ref _balance, amount);
        }

        public void DecreaseAccountBalance(long amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount can not be lower than 0.", nameof(amount));
            }

            if (!CanTransferFunds(amount))
            {
                throw new ArgumentException("Insufficient funds.", nameof(amount));
            }

            Interlocked.Add(ref _balance, -amount);
        }

        private bool CanTransferFunds(long amount) => Balance >= amount;

        public override string ToString()
            => $"User: {Name}{Environment.NewLine}" +
               $"Private key:{PrivateKey.ToString().ToLowerInvariant()}{Environment.NewLine}AccountBalance: {Balance}";

        public bool Equals(Account other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Account) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}