using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StateBank.DTOs;
using StateBank.Services;

namespace StateBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="createAccountDto">Account creation details</param>
        /// <returns>Created account details</returns>
        [HttpPost]
        public async Task<ActionResult<AccountDto>> CreateAccount([FromBody] CreateAccountDto createAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountDto = await _accountService.CreateAccountAsync(createAccountDto);
            return CreatedAtAction(nameof(GetAccountById), new { id = accountDto.Id }, accountDto);
        }

        /// <summary>
        /// Retrieves all accounts
        /// </summary>
        /// <returns>List of all accounts</returns>
        [HttpGet]
        public async Task<ActionResult<List<AccountDto>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        /// <summary>
        /// Retrieves a specific account by Id
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <returns>Account details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            return Ok(account);
        }

        /// <summary>
        /// Deposits amount to an account
        /// </summary>
        /// <param name="transactionDto">Account Id and deposit amount</param>
        /// <returns>Updated account details</returns>
        [HttpPost("deposit")]
        public async Task<ActionResult<AccountDto>> Deposit([FromBody] TransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _accountService.DepositAsync(transactionDto);
            return Ok(account);
        }

        /// <summary>
        /// Withdraws amount from an account
        /// </summary>
        /// <param name="transactionDto">Account Id and withdrawal amount</param>
        /// <returns>Updated account details</returns>
        [HttpPost("withdraw")]
        public async Task<ActionResult<AccountDto>> Withdraw([FromBody] TransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _accountService.WithdrawAsync(transactionDto);
            return Ok(account);
        }
    }
}
