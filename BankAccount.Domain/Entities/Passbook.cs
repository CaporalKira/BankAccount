using BankAccount.Domain.Enums;

namespace BankAccount.Domain.Entities
{
    public class Passbook : Account
    {
        public decimal DepositLimit { get; private set; }

        public override AccountType AccountType => AccountType.Passbook;

        public Passbook(string accountNumber, decimal initialBalance, decimal depositLimit)
            : base(accountNumber, initialBalance, null)
        {
            DepositLimit = depositLimit;
        }

        public override void Deposit(decimal depositAmount)
        {
            if (depositAmount <= 0)
                throw new ArgumentException("The amount should be greater than zero");

            if (Balance + depositAmount > DepositLimit)
                throw new ArgumentException("You cannot exceed the deposit limit.");

            base.Deposit(depositAmount);
        }
    }
}
