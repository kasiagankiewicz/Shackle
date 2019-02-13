using System;

namespace Shackle.Core.Models
{
    public class Transaction
    {
        public Account Sender { get; }
        public Account Receiver { get; }
        public long Amount { get; }
        public Signature Signature { get; private set; }

        public Transaction(Account sender, Account receiver, long amount, Signature signature)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Transaction amount must be greater than 0.",
                    nameof(amount));
            }

            Sender = sender ?? throw new ArgumentNullException(nameof(sender),
                         "Sender can not be null");
            Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver),
                           "Receiver can not be null");
            Amount = amount;
            Signature = signature ?? throw new ArgumentNullException(nameof(signature),
                            "Signature can not be null");
            sender.DecreaseAccountBalance(amount);
            receiver.IncreaseAccountBalance(amount);
        }

        public override string ToString()
            => $"From: {Sender.Name}{Environment.NewLine}To: {Receiver.Name}{Environment.NewLine}" +
               $"Amount: {Amount}{Environment.NewLine}Signature: {Signature}";
    }
}