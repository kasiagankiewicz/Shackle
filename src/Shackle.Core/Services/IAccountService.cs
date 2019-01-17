using System.Collections.Generic;
using Shackle.Core.Models;

namespace Shackle.Core.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAll();
        Account Get(string name);
        void Create(string name, long balance = 100);
    }
}