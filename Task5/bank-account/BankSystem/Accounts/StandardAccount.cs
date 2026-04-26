using BankSystem.Generators;

namespace BankSystem.Accounts;

public sealed class StandardAccount : BankAccount
{
    private const decimal StandardBalanceCostPerPoint = 100m;

    public StandardAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    public override decimal Overdraft => 0m;

    protected override int CalculateDepositRewardPoints(decimal amount)
        => Math.Max((int)Math.Floor(this.Balance / StandardBalanceCostPerPoint), 0);

    protected override int CalculateWithdrawRewardPoints(decimal amount)
        => Math.Max((int)Math.Floor(this.Balance / StandardBalanceCostPerPoint), 0);
}
