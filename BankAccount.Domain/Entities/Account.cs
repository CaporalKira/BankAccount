using BankAccount.Domain.Enums;

namespace BankAccount.Domain.Entities
{
    public class Account
    {
        public string AccountNumber { get; }

        public decimal Balance { get; private set; }

        public decimal? AuthorizedOverdraft { get; private set; }

        public virtual AccountType AccountType => AccountType.Account;

        public List<Operation> Operations { get; } = new();

        public Account(string accountNumber, decimal initialBalance, decimal? authorizedOverdraft = null)
        {
            if (initialBalance < 0)
                throw new ArgumentException("The initial balance should be positive");

            if (authorizedOverdraft < 0)
                throw new ArgumentException("The authorized overdraft should be positive");

            AccountNumber = accountNumber;
            Balance = initialBalance;
            AuthorizedOverdraft = authorizedOverdraft;
        }

        public virtual void Deposit(decimal depositAmount)
        {
            if (depositAmount <= 0)
                throw new ArgumentException("The amount should be greater than zero");
            Balance += depositAmount;
            Operations.Add(new Operation(AccountType, OperationType.Deposit, depositAmount));
        }

        public void Withdraw(decimal withdrawAmount)
        {
            if (withdrawAmount <= 0)
                throw new ArgumentException("The amount should be greater than zero");

            decimal finalBalance = Balance - withdrawAmount;
            if (!AuthorizedOverdraft.HasValue && finalBalance < 0)
                throw new ArgumentException("The balance is insufficient for this withdrawal");

            if (AuthorizedOverdraft.HasValue && finalBalance < -AuthorizedOverdraft)
                throw new ArgumentException($"The balance is insufficient for this withdrawal (authorized overdraft: {AuthorizedOverdraft})");

            Balance = finalBalance;
            Operations.Add(new Operation(AccountType, OperationType.Withdrawal, withdrawAmount));
        }
    }
}
