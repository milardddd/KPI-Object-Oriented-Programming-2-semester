using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using BankSystem.Generators;
using BankSystem.Helpers;

namespace BankSystem.Accounts;

public abstract class BankAccount
{
    private readonly List<AccountCashOperation> operations = [];

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : this(owner, currencyCode, GetNumberGenerator(uniqueNumberGenerator))
    {
    }

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
    {
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(numberGenerator);
        ValidateCurrencyCode(currencyCode);

        this.Owner = owner;
        this.CurrencyCode = currencyCode;
        this.Number = numberGenerator();

        this.Owner.Add(this);
    }

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : this(owner, currencyCode, uniqueNumberGenerator)
    {
        this.Deposit(initialBalance, DateTime.Now, "Initial balance");
    }

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : this(owner, currencyCode, numberGenerator)
    {
        this.Deposit(initialBalance, DateTime.Now, "Initial balance");
    }

    public string Number { get; }

    public decimal Balance { get; private set; }

    public string CurrencyCode { get; }

    public AccountOwner Owner { get; }

    public int BonusPoints { get; private set; }

    public abstract decimal Overdraft { get; }

    [SuppressMessage("Design", "CA1024:Use properties where appropriate", Justification = "API contract requires a method.")]
    public ReadOnlyCollection<AccountCashOperation> GetAllOperations() => this.operations.AsReadOnly();

    public void Deposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        this.Balance += amount;
        this.BonusPoints += this.CalculateDepositRewardPoints(amount);
        this.operations.Add(new AccountCashOperation(amount, date, note));
    }

    public void Withdraw(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        if ((this.Balance - amount) < -this.Overdraft)
        {
            throw new InvalidOperationException("Insufficient funds considering overdraft.");
        }

        this.Balance -= amount;
        this.BonusPoints += this.CalculateWithdrawRewardPoints(amount);
        this.operations.Add(new AccountCashOperation(-amount, date, note));
    }

    public override string ToString() => $"{this.Owner} No:{this.Number}. Balance: {this.Balance}{this.CurrencyCode}.";

    protected abstract int CalculateDepositRewardPoints(decimal amount);

    protected abstract int CalculateWithdrawRewardPoints(decimal amount);

    private static void ValidateCurrencyCode(string currencyCode)
    {
        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            throw new ArgumentException("Currency code cannot be empty.", nameof(currencyCode));
        }

        if (!currencyCode.IsCurrencyValid())
        {
            throw new ArgumentException("Invalid currency code.", nameof(currencyCode));
        }
    }

    private static Func<string> GetNumberGenerator(IUniqueNumberGenerator uniqueNumberGenerator)
    {
        ArgumentNullException.ThrowIfNull(uniqueNumberGenerator);
        return uniqueNumberGenerator.Generate;
    }
}
