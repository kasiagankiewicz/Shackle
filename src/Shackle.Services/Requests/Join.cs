using System.ComponentModel.DataAnnotations;

namespace Shackle.Services.Requests
{
    public class Join
    {
        [Required]
        public string Name { get; }
        [Range(1,10000000)]
        public long? Balance { get; }

        public Join(string name, long? balance)
        {
            Name = name;
            Balance = balance;
        }
    }
}