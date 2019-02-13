using Shackle.Core.Models;

namespace Shackle.Services.Dto
{
    public class TransactionDto
    {
        public AccountDto Sender { get; set; }
        public AccountDto Receiver { get; set; }
        public long Amount { get; set; }
        public string Signature { get; set; }

        public TransactionDto()
        {
        }

        public TransactionDto(Transaction transaction)
        {
            Sender = new AccountDto(transaction.Sender);
            Receiver = new AccountDto(transaction.Receiver);
            Amount = transaction.Amount;
            Signature = transaction.Signature.ToString();
        }
    }
}




