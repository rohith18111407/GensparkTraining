using BasicBankingApplication.DTOs;
using BasicBankingApplication.Exceptions;
using BasicBankingApplication.Interfaces;
using BasicBankingApplication.Models;

namespace BasicBankingApplication.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Account> CreateAccountAsync(AccountCreateDto accountDto)
        {

            if (accountDto.InitialDeposit < 500)
                throw new ArgumentException("Initial deposit must be at least 500.");

            var account = new Account
            {
                AccountHolderName = accountDto.AccountHolderName,
                Balance = accountDto.InitialDeposit
            };

            return await _repository.CreateAccountAsync(account);
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _repository.GetAccountByIdAsync(accountId);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _repository.GetAllAccountsAsync();
        }

        public async Task DepositAsync(int accountId, decimal amount)
        {
            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                var account = await _repository.GetAccountByIdAsync(accountId);
                if (account == null)
                    throw new KeyNotFoundException("Account not found.");

                account.Balance += amount;
                await _repository.UpdateAccountAsync(account);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task WithdrawAsync(int accountId, decimal amount)
        {
            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                var account = await _repository.GetAccountByIdAsync(accountId);
                if (account == null)
                    throw new KeyNotFoundException("Account not found.");

                if (account.Balance < amount)
                    throw new InsufficientFundsException("Insufficient funds.");

                account.Balance -= amount;
                await _repository.UpdateAccountAsync(account);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task TransferAsync(TransferDto transferDto)
        {
            if (transferDto.Amount <= 0)
                throw new ArgumentException("Transfer amount must be greater than zero.");

            if (transferDto.FromAccountId == transferDto.ToAccountId)
                throw new ArgumentException("Cannot transfer to the same account.");

            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                var fromAccount = await _repository.GetAccountByIdAsync(transferDto.FromAccountId);
                var toAccount = await _repository.GetAccountByIdAsync(transferDto.ToAccountId);

                if (fromAccount == null || toAccount == null)
                    throw new KeyNotFoundException("One or both accounts not found.");

                if (fromAccount.Balance < transferDto.Amount)
                    throw new InsufficientFundsException("Insufficient funds for transfer.");

                if (fromAccount.Balance - transferDto.Amount < 500)
                    throw new InvalidOperationException("Insufficient balance after maintaining ₹500 minimum requirement.");

                fromAccount.Balance -= transferDto.Amount;
                toAccount.Balance += transferDto.Amount;

                await _repository.UpdateAccountAsync(fromAccount);
                await _repository.UpdateAccountAsync(toAccount);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
