using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Shackle.Services.Requests
{
    public class CreateTransaction
    {
        [Required]
        public string Sender { get; }
        [Required]
        public string Receiver { get; }
        [Range(1,10000000)]
        public long Amount { get; }

        [JsonConstructor]
        public CreateTransaction(string sender, string receiver, long amount)
        {
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
        }
    }
}