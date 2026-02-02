using BankAccount.Domain.Entities;
using BankAccount.Domain.Enums;
using FluentAssertions;

namespace BankAccount.Tests.EntitiesTests.PassbookTests
{
    public class PassbookTests
    {
        [Fact]
        public void Deposit_Should_Return_Correct_Balance_After_Deposit()
        {
            // Arrange
            var depositAmount = 100;
            var initialBalance = 1000;
            var depositLimit = 22950;
            var accountNumber = "FR134567890";
            var passbook = new Passbook(accountNumber, initialBalance, depositLimit);

            // Act
            passbook.Deposit(depositAmount);

            // Assert
            passbook.Balance.Should().Be(1100);
        }

        [Fact]
        public void Deposit_Should_Add_Operation_To_Operations_List()
        {
            // Arrange
            var depositAmount = 100;
            var initialBalance = 1000;
            var depositLimit = 22950;
            var accountNumber = "FR134567890";
            var passbook = new Passbook(accountNumber, initialBalance, depositLimit);

            // Act
            passbook.Deposit(depositAmount);

            // Assert
            passbook.Operations.Should().HaveCount(1);
            passbook.Operations[0].OperationType.Should().Be(OperationType.Deposit);
            passbook.Operations[0].Amount.Should().Be(depositAmount);
        }

        [Fact]
        public void Deposit_Should_Throw_Error_When_Deposit_Amount_Is_Less_Than_Zero()
        {
            // Arrange
            var depositAmount = -100;
            var initialBalance = 1000;
            var depositLimit = 22950;
            var accountNumber = "FR134567890";
            var passbook = new Passbook(accountNumber, initialBalance, depositLimit);

            // Act
            Action action = () => passbook.Deposit(depositAmount);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("The amount should be greater than zero");
        }

        [Fact]
        public void Deposit_Should_Throw_Error_When_Exceeds_Deposite_Limit()
        {
            // Arrange
            var depositAmount = 100;
            var initialBalance = 22900;
            var depositLimit = 22950;
            var accountNumber = "FR134567890";
            var passbook = new Passbook(accountNumber, initialBalance, depositLimit);

            // Act
            Action action = () => passbook.Deposit(depositAmount);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("You cannot exceed the deposit limit.");
        }

        [Fact]
        public void Withdraw_Should_Return_Correct_Balance_After_Withdrawal()
        {
            // Arrange
            var withdrawalAmount = 100;
            var initialBalance = 1000;
            var accountNumber = "FR134567890";
            var depositLimit = 22950;
            var passbook = new Passbook(accountNumber, initialBalance, depositLimit);

            // Act 
            passbook.Withdraw(withdrawalAmount);

            // Assert 
            passbook.Balance.Should().Be(900);
        }

        [Fact]
        public void Withdraw_Should_Add_Operation_To_Operations_List()
        {
            // Arrange
            var withdrawalAmount = 100;
            var initialBalance = 1000;
            var depositLimit = 22950;
            var accountNumber = "FR134567890";
            var passbook = new Passbook(accountNumber, initialBalance, depositLimit);

            // Act
            passbook.Withdraw(withdrawalAmount);

            // Assert
            passbook.Operations.Should().HaveCount(1);
            passbook.Operations[0].OperationType.Should().Be(OperationType.Withdrawal);
            passbook.Operations[0].Amount.Should().Be(withdrawalAmount);
        }

        [Fact]
        public void Withdraw_Should_Return_Error_If_Balance_Is_Less_Than_Withdrawal_Amount()
        {
            // Arrange
            var withdrawalAmount = 100;
            var initialBalance = 90;
            var accountNumber = "FR134567890";
            var depositLimit = 22950;
            var passbook = new Passbook(accountNumber, initialBalance, depositLimit);

            // Act
            Action action = () => passbook.Withdraw(withdrawalAmount);

            // Assert 
            action.Should().Throw<ArgumentException>().WithMessage("The balance is insufficient for this withdrawal");
        }
    }
}
