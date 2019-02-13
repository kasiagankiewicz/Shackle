using System.Collections.Generic;
using Shackle.Core.Models;

namespace Shackle.Core.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
        Account Get(string name);
        void Add(Account account);
    }
}