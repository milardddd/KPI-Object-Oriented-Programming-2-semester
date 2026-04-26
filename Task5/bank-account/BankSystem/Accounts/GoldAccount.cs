using BankSystem.Generators;

namespace BankSystem.Accounts;

public sealed class GoldAccount : BankAccount
{
    private const decimal GoldDepositCostPerPoint = 5m;
    private const decimal GoldWithdrawCostPerPoint = 2m;
    private const decimal GoldBalanceCostPerPoint = 10m;
    private const decimal GoldDepositPremiumFactor = 11.2m;
    private const decimal GoldWithdrawPremiumFactor = 6.45m;

    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    public override decimal Overdraft => this.BonusPoints * 3m;

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        int basePoints = (int)Math.Ceiling((this.Balance / GoldBalanceCostPerPoint) + (amount / GoldDepositCostPerPoint));
        int premiumPoints = (int)Math.Floor(amount / GoldDepositPremiumFactor);
        return Math.Max(basePoints + premiumPoints, 0);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        int basePoints = (int)Math.Ceiling((this.Balance / GoldBalanceCostPerPoint) + (amount / GoldWithdrawCostPerPoint));
        int premiumPoints = (int)Math.Floor(amount / GoldWithdrawPremiumFactor);
        return Math.Max(basePoints + premiumPoints, 0);
    }
}
