using BankAccount.Domain.Entities;
using BankAccount.Domain.Enums;

namespace BankAccount.Domain.ObjectValues
{
    public class AccountStatement
    {
        public AccountType AccountType { get; }
        public decimal BalanceAtIssue { get; }
        public DateTimeOffset IssueDate { get; }
        public DateTimeOffset PeriodStart { get; }
        public DateTimeOffset PeriodEnd { get; }
        public IReadOnlyCollection<Operation> Operations { get; }
        public AccountStatement(AccountType accountType, decimal balanceAtIssue, DateTimeOffset issueDate, DateTimeOffset periodStart, DateTimeOffset periodEnd, IReadOnlyCollection<Operation> operations)
        {
            AccountType = accountType;
            BalanceAtIssue = balanceAtIssue;
            IssueDate = issueDate;
            PeriodStart = periodStart;
            PeriodEnd = periodEnd;
            Operations = operations;
        }
    }
}
