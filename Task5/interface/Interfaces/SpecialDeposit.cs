using System;

namespace Interfaces;

public class SpecialDeposit : Deposit, IProlongable
{
    public SpecialDeposit(decimal amount, int period)
        : base(amount, period)
    {
    }

    public override decimal Income()
    {
        decimal currentSum = this.Amount;
        for (int i = 1; i <= this.Period; i++)
        {
            currentSum += currentSum * (i / 100m);
        }

        return currentSum - this.Amount;
    }

    public bool CanToProlong() => this.Amount > 1000m;
}