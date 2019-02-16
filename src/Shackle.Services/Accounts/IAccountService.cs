using System.Collections.Generic;
using Shackle.Services.Dto;

namespace Shackle.Services.Accounts
{
    public interface IAccountService
    {
        IEnumerable<AccountDto> GetAll();
        AccountDetailsDto Get(string name);
        void Add(string name, long balance = 100);
    }
}