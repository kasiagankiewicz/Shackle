using System;
using System.Linq;
using Shackle.Core.Models;

namespace Shackle.Core.Services
{
    public class TransactionService : ITransactionService
    {
        public Transaction Execute(Account sender, Account receiver, long amount, ISigner signer)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender), "Sender cannot be null.");
            }

            if (receiver is null)
            {
                throw new ArgumentNullException(nameof(receiver), "Receiver cannot be null.");
            }

            var data = sender.Address.Bytes.Union(receiver.Address.Bytes);
            var signature = signer.Sign(data, sender.PrivateKey);

            return new Transaction(sender, receiver, amount, signature);
        }
    }
}