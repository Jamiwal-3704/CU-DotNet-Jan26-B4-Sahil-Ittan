using AutoMapper;
using StateBank.DTOs;
using StateBank.Exceptions;
using StateBank.Helpers;
using StateBank.Models;
using StateBank.Repositories;

namespace StateBank.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        // Business rule: Minimum balance ₹1000
        private const decimal MINIMUM_BALANCE = 1000m;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new account with validation
        /// </summary>
        public async Task<AccountDto> CreateAccountAsync(CreateAccountDto createAccountDto)
        {
            // Validation: Minimum deposit ₹1000
            if (createAccountDto.InitialDeposit < MINIMUM_BALANCE)
            {
                throw new BadRequestException($"Minimum deposit amount is ₹{MINIMUM_BALANCE}");
            }

            // Map DTO to Account entity
            var account = _mapper.Map<Account>(createAccountDto);
            account.Balance = createAccountDto.InitialDeposit;

            // Save to database
            var createdAccount = await _accountRepository.CreateAsync(account);

            // Generate account number after save (using the auto-generated Id)
            createdAccount.AccountNumber = AccountNumberGenerator.Generate(createdAccount.Id);

            // Update with account number
            await _accountRepository.UpdateAsync(createdAccount);

            return _mapper.Map<AccountDto>(createdAccount);
        }

        /// <summary>
        /// Retrieves all accounts
        /// </summary>
        public async Task<List<AccountDto>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return _mapper.Map<List<AccountDto>>(accounts);
        }

        /// <summary>
        /// Retrieves a specific account by Id
        /// </summary>
        public async Task<AccountDto> GetAccountByIdAsync(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);

            if (account == null)
            {
                throw new NotFoundException($"Account with Id {id} not found");
            }

            return _mapper.Map<AccountDto>(account);
        }

        /// <summary>
        /// Deposits amount to an account
        /// </summary>
        public async Task<AccountDto> DepositAsync(TransactionDto transactionDto)
        {
            // Validation: Amount must be > 0
            if (transactionDto.Amount <= 0)
            {
                throw new BadRequestException("Deposit amount must be greater than 0");
            }

            var account = await _accountRepository.GetByIdAsync(transactionDto.AccountId);

            if (account == null)
            {
                throw new NotFoundException($"Account with Id {transactionDto.AccountId} not found");
            }

            // Add amount to balance
            account.Balance += transactionDto.Amount;

            // Update account
            await _accountRepository.UpdateAsync(account);

            return _mapper.Map<AccountDto>(account);
        }

        /// <summary>
        /// Withdraws amount from an account
        /// </summary>
        public async Task<AccountDto> WithdrawAsync(TransactionDto transactionDto)
        {
            // Validation: Amount must be > 0
            if (transactionDto.Amount <= 0)
            {
                throw new BadRequestException("Withdrawal amount must be greater than 0");
            }

            var account = await _accountRepository.GetByIdAsync(transactionDto.AccountId);

            if (account == null)
            {
                throw new NotFoundException($"Account with Id {transactionDto.AccountId} not found");
            }

            // Check minimum balance rule
            if (account.Balance - transactionDto.Amount < MINIMUM_BALANCE)
            {
                throw new BadRequestException(
                    $"Cannot withdraw. Minimum balance must be ₹{MINIMUM_BALANCE}. Current balance: ₹{account.Balance}");
            }

            // Deduct amount from balance
            account.Balance -= transactionDto.Amount;

            // Update account
            await _accountRepository.UpdateAsync(account);

            return _mapper.Map<AccountDto>(account);
        }
    }
}
