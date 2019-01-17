using System;
using System.Collections.Generic;
using System.Linq;
using Shackle.Core.Factories;
using Shackle.Core.Models;

namespace Shackle.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly ICryptoFactory _cryptoFactory;
        private readonly ISet<Account> _users = new HashSet<Account>();

        public AccountService(ICryptoFactory cryptoFactory)
        {
            _cryptoFactory = cryptoFactory;
        }

        public IEnumerable<Account> GetAll() => _users;

        public Account Get(string name)
            => _users.SingleOrDefault(u => u.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public void Create(string name, long balance = 100)
        {
//            _users.Any(u => u.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)); // T|F
            var user = Get(name);
            if (user != null)
            {
                throw new ArgumentException($"User with name {name} already exists.", nameof(name));
            }

            var (privateKey, publicKey, address) = _cryptoFactory.Create();
            user = new Account(name, privateKey, publicKey, address, balance);
            _users.Add(user);
        }
    }
}      