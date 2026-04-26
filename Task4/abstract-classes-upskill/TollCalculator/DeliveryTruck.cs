using System;

namespace TollCalculator;

public class DeliveryTruck : Vehicle
{
    private int grossWeightClass;

    public DeliveryTruck(decimal baseToll, int grossWeightClass)
        : base(baseToll)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(grossWeightClass);
        this.grossWeightClass = grossWeightClass;
    }

    public int GrossWeightClass
    {
        get => this.grossWeightClass;
    }

    protected override decimal Calculate()
    {
        if (this.GrossWeightClass > 5000)
        {
            return this.BaseToll + 5.00m;
        }

        if (this.GrossWeightClass < 3000)
        {
            return this.BaseToll - 2.00m;
        }

        return this.BaseToll;
    }
}
