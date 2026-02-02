using BankAccount.Domain.Entities;
using BankAccount.Domain.Interfaces;
using BankAccount.Domain.ObjectValues;

namespace BankAccount.Domain.Services
{
    public class AccountStatementService: IAccountStatementService
    {
        public AccountStatement GenerateAccountStatement(Account account, DateTimeOffset issueDate)
        {
            DateTimeOffset periodStart = issueDate.AddMonths(-1);
            DateTimeOffset periodEnd = issueDate;

            List<Operation> operations = account.Operations
                .Where(op => op.OperationDate >= periodStart && op.OperationDate <= periodEnd)
                .OrderByDescending(op => op.OperationDate)
                .ToList();

            return new AccountStatement(
                account.AccountType,
                account.Balance,
                issueDate,
                periodStart,
                periodEnd,
                operations
            );
        }
    }
}
