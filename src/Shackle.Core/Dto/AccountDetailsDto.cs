using Shackle.Core.Models;

namespace Shackle.Core.Dto
{
    public class AccountDetailsDto : AccountDto
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }

        public AccountDetailsDto()
        {
        }

        public AccountDetailsDto(Account account) : base(account)
        {
            PrivateKey = account.PrivateKey.ToString();
            PublicKey = account.PublicKey.ToString();
        }
    }
}