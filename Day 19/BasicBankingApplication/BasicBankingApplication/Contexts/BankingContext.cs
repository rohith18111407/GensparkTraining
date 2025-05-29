using BasicBankingApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicBankingApplication.Contexts
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
