using System;

namespace Interfaces;

public class BaseDeposit : Deposit
{
    public BaseDeposit(decimal amount, int period)
        : base(amount, period)
    {
    }

    public override decimal Income()
    {
        decimal currentSum = this.Amount;
        for (int i = 0; i < this.Period; i++)
        {
            currentSum += currentSum * 0.05m;
        }

        return currentSum - this.Amount;
    }
}