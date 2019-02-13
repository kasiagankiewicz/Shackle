using System;
using System.Collections.Generic;
using System.Linq;
using Shackle.Core.Models;
using Shackle.Core.Repositories;

namespace Shackle.Infrastructure.Persistence.InMemory
{
    public class InMemoryAccountRepository : IAccountRepository
    {
        private readonly ISet<Account> _accounts = new HashSet<Account>();

        public IEnumerable<Account> GetAll() => _accounts;

        public Account Get(string name)
            => _accounts.SingleOrDefault(a => string.Equals(a.Name, name, StringComparison.InvariantCultureIgnoreCase));

        public void Add(Account account)
        {
            _accounts.Add(account);
        }
    }
}