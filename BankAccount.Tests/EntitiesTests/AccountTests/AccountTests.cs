using BankAccount.Domain.Entities;
using BankAccount.Domain.Enums;
using FluentAssertions;

namespace BankAccount.Tests.EntitiesTests.AccountTests
{
    public class AccountTests
    {
        [Fact]
        public void Deposit_Should_Return_Correct_Balance_After_Deposit()
        {
            // Arrange
            var depositAmount = 100;
            var initialBalance = 1000;
            var accountNumber = "FR134567890";
            var account = new Account(accountNumber, initialBalance);

            // Act
            account.Deposit(depositAmount);

            // Assert
            account.Balance.Should().Be(1100);
        }

        [Fact]
        public void Deposit_Should_Add_Operation_To_Operations_List()
        {
            // Arrange
            var depositAmount = 100;
            var initialBalance = 1000;
            var accountNumber = "FR134567890";
            var account = new Account(accountNumber, initialBalance);

            // Act
            account.Deposit(depositAmount);

            // Assert
            account.Operations.Should().HaveCount(1);
            account.Operations[0].OperationType.Should().Be(OperationType.Deposit);
            account.Operations[0].Amount.Should().Be(depositAmount);
        }

        [Fact]
        public void Deposit_Should_Throw_Error_When_Deposit_Amount_Is_Less_Than_Zero()
        {
            // Arrange
            var depositAmount = -100;
            var initialBalance = 1000;
            var accountNumber = "FR134567890";
            var account = new Account(accountNumber, initialBalance);

            // Act
            Action action = () => account.Deposit(depositAmount);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("The amount should be greater than zero");
        }

        [Fact]
        public void Withdraw_Should_Return_Correct_Balance_After_Withdrawal()
        {
            // Arrange
            var withdrawalAmount = 100;
            var initialBalance = 1000;
            var accountNumber = "FR134567890";
            var account = new Account(accountNumber, initialBalance);

            // Act 
            account.Withdraw(withdrawalAmount);

            // Assert 
            account.Balance.Should().Be(900);
        }

        [Fact]
        public void Withdraw_Should_Add_Operation_To_Operations_List()
        {
            // Arrange
            var withdrawalAmount = 100;
            var initialBalance = 1000;
            var accountNumber = "FR134567890";
            var account = new Account(accountNumber, initialBalance);

            // Act
            account.Withdraw(withdrawalAmount);

            // Assert
            account.Operations.Should().HaveCount(1);
            account.Operations[0].OperationType.Should().Be(OperationType.Withdrawal);
            account.Operations[0].Amount.Should().Be(withdrawalAmount);
        }

        [Fact]
        public void Withdraw_Without_Authorized_Overdraft_Should_Return_Error_If_Balance_Is_Less_Than_Withdrawal_Amount()
        {
            // Arrange
            var withdrawalAmount = 100;
            var initialBalance = 90;
            var accountNumber = "FR134567890";
            var account = new Account(accountNumber, initialBalance);

            // Act
            Action action = () => account.Withdraw(withdrawalAmount);

            // Assert 
            action.Should().Throw<ArgumentException>().WithMessage("The balance is insufficient for this withdrawal");
        }

        [Fact]
        public void Withdraw_With_Authorized_Overdraft_Should_Return_Error_If_Exceeds_Limit() {

            // Arrange
            var withdrawalAmount = 200;
            var initialBalance = 100;
            var authorizedOverdraft = 50;
            var accountNumber = "FR134567890";
            var account = new Account(accountNumber, initialBalance, authorizedOverdraft);

            // Act
            Action action = () => account.Withdraw(withdrawalAmount);

            // Assert 
            action.Should().Throw<ArgumentException>().WithMessage($"The balance is insufficient for this withdrawal (authorized overdraft: {authorizedOverdraft})");
        }

        [Fact]
        public void Withdraw_Should_Return_Error_If_Withdraw_Amount_Is_Negative()
        {
            // Arrange
            var withdrawalAmount = -200;
            var initialBalance = 100;
            var authorizedOverdraft = 50;
            var accountNumber = "FR134567890";
            var account = new Account(accountNumber, initialBalance, authorizedOverdraft);

            // Act
            Action action = () => account.Withdraw(withdrawalAmount);

            // Assert 
            action.Should().Throw<ArgumentException>().WithMessage("The amount should be greater than zero");
        }
    }
}
