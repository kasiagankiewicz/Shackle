using Newtonsoft.Json;

namespace Shackle.Core.Requests
{
    public class CreateTransfer
    {
        public string Sender { get; }
        public string Receiver { get; }
        public long Amount { get; }

        [JsonConstructor]
        public CreateTransfer(string sender, string receiver, long amount)
        {
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
        }
    }
}