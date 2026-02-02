using BankAccount.Domain.Entities;
using BankAccount.Domain.Enums;
using BankAccount.Domain.Interfaces;
using BankAccount.Domain.Services;
using FluentAssertions;

namespace BankAccount.Tests.ServicesTests.MounthlyAccountStatementTests
{
    public class AccountStatementServiceTests
    {
        private readonly IAccountStatementService _statement;

        public AccountStatementServiceTests()
        {
            _statement = new AccountStatementService();
        }

        [Fact]
        public void GenerateAccountStatement_Should_Include_Only_Operations_Within_Last_Month_And_Order_Them_Descending()
        {
            // Arrange
            var now = DateTimeOffset.UtcNow;
            Account account = new Account("FR1234567890", 1000);

            account.Deposit(50);
            account.Deposit(500);
            account.Withdraw(200);

            // Act
            var accountStatement = _statement.GenerateAccountStatement(account, now);

            //Assert
            accountStatement.AccountType.Should().Be(AccountType.Account);
            accountStatement.BalanceAtIssue.Should().Be(account.Balance);
            accountStatement.Operations.Should().BeInDescendingOrder(item => item.OperationDate);
        }
    }   
}
