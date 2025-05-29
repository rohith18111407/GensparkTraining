using BasicBankingApplication.DTOs;
using BasicBankingApplication.Exceptions;
using BasicBankingApplication.Interfaces;
using BasicBankingApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicBankingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountsController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateDto accountDto)
        {
            try
            {
                var createdAccount = await _service.CreateAccountAsync(accountDto);
                return Ok(createdAccount);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the account.", details = ex.Message });
            }
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccountById(int accountId)
        {
            var account = await _service.GetAccountByIdAsync(accountId);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _service.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> Deposit(int accountId, [FromBody] DepositDto depositDto)
        {
            try
            {
                await _service.DepositAsync(accountId, depositDto.Amount);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{accountId}/withdraw")]
        public async Task<IActionResult> Withdraw(int accountId, [FromBody] WithdrawDto withdrawDto)
        {
            try
            {
                await _service.WithdrawAsync(accountId, withdrawDto.Amount);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InsufficientFundsException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto transferDto)
        {
            try
            {
                await _service.TransferAsync(transferDto);
                return Ok(new { message = "Transfer successful." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An internal server error occurred.", details = ex.Message });
            }
        }
    }
   
}
