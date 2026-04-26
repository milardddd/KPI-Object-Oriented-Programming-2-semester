using System;

namespace Interfaces;

public class LongDeposit : Deposit, IProlongable
{
    public LongDeposit(decimal amount, int period)
        : base(amount, period)
    {
    }

    public override decimal Income()
    {
        if (this.Period <= 6)
        {
            return 0;
        }

        decimal currentSum = this.Amount;
        for (int i = 7; i <= this.Period; i++)
        {
            currentSum += currentSum * 0.15m;
        }

        return currentSum - this.Amount;
    }

    public bool CanToProlong() => this.Period <= 36;
}