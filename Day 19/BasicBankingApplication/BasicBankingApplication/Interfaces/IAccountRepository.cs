using BasicBankingApplication.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace BasicBankingApplication.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task UpdateAccountAsync(Account account);
        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}
