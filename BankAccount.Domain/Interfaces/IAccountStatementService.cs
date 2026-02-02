using BankAccount.Domain.Entities;
using BankAccount.Domain.ObjectValues;

namespace BankAccount.Domain.Interfaces
{
    public interface IAccountStatementService
    {
        AccountStatement GenerateAccountStatement(Account account, DateTimeOffset issueDate);
    }
}
