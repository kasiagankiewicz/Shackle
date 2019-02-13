using System.Collections.Generic;
using System.Linq;
using Shackle.Core.Factories;
using Shackle.Core.Models;
using Shackle.Core.Repositories;
using Shackle.Services.Dto;

namespace Shackle.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly ICryptoFactory _cryptoFactory;
        private readonly IAccountRepository _accountRepository;

        public AccountService(ICryptoFactory cryptoFactory,
            IAccountRepository accountRepository)
        {
            _cryptoFactory = cryptoFactory;
            _accountRepository = accountRepository;
        }

        public IEnumerable<AccountDto> GetAll() => _accountRepository.GetAll().Select(a => new AccountDto(a));

        public AccountDetailsDto Get(string name)
        {
            var account = _accountRepository.Get(name);

            return account is null ? null : new AccountDetailsDto(account);
        }

        public void Join(string name, long balance = 100)
        {
            var account = _accountRepository.Get(name);
            if (account != null)
            {
                return;
//                throw new ArgumentException($"Account with name {name} already exists.", nameof(name));
            }

            var (privateKey, publicKey, address) = _cryptoFactory.Create();
            account = new Account(name, privateKey, publicKey, address, balance);
            _accountRepository.Add(account);
        }
    }
}      