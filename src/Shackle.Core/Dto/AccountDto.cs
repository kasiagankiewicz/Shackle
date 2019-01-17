using Shackle.Core.Models;

namespace Shackle.Core.Dto
{
    public class AccountDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public long Balance { get; set; }

        public AccountDto()
        {
        }

        public AccountDto(Account account)
        {
            Name = account.Name;
            Address = account.Address.ToString();
            Balance = account.Balance;
        }
    }
}