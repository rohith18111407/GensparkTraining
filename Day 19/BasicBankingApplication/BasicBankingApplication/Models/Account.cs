using System.ComponentModel.DataAnnotations;

namespace BasicBankingApplication.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        public string AccountHolderName { get; set; }
        [Required]
        public decimal Balance { get; set; }
    }
}
