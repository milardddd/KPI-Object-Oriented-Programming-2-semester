using BankSystem.Generators;

namespace BankSystem.Accounts;

public sealed class SilverAccount : BankAccount
{
    private const decimal SilverDepositCostPerPoint = 5m;
    private const decimal SilverWithdrawCostPerPoint = 2m;
    private const decimal SilverBalanceCostPerPoint = 100m;

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    public override decimal Overdraft => this.BonusPoints * 2m;

    protected override int CalculateDepositRewardPoints(decimal amount)
        => Math.Max((int)Math.Floor((this.Balance / SilverBalanceCostPerPoint) + (amount / SilverDepositCostPerPoint)), 0);

    protected override int CalculateWithdrawRewardPoints(decimal amount)
        => Math.Max((int)Math.Floor((this.Balance / SilverBalanceCostPerPoint) + (amount / SilverWithdrawCostPerPoint)), 0);
}
