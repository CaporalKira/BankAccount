using BankAccount.Domain.Enums;

namespace BankAccount.Domain.Entities
{
    public class Operation
    {
        public Guid Id { get;}

        public AccountType AccountType { get;}

        public OperationType OperationType { get;}

        public decimal Amount { get;}

        public DateTimeOffset OperationDate { get;}

        public Operation (AccountType accountType, OperationType operationType, decimal amount)
        {
            Id = Guid.NewGuid();
            AccountType = accountType;
            OperationType = operationType;
            Amount = amount;
            OperationDate = DateTimeOffset.UtcNow;
        }
    }
}
