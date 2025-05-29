using BasicBankingApplication.DTOs;
using BasicBankingApplication.Models;

namespace BasicBankingApplication.Interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(AccountCreateDto accountDto);
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task DepositAsync(int accountId, decimal amount);
        Task WithdrawAsync(int accountId, decimal amount);
        Task TransferAsync(TransferDto transferDto);
    }
}
