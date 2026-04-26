using System;

namespace TollCalculator;

public class Bus : Vehicle
{
    private int capacity;
    private int passengers;

    public Bus(decimal basicToll, int capacity, int passengers)
        : base(basicToll)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        ArgumentOutOfRangeException.ThrowIfNegative(passengers);

        this.capacity = capacity;
        this.passengers = passengers;
    }

    public int Capacity
    {
        get => this.capacity;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
            this.capacity = value;
        }
    }

    public int Passengers
    {
        get => this.passengers;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            this.passengers = value;
        }
    }

    protected override decimal Calculate()
    {
        double filling = (double)this.Passengers / this.Capacity;

        if (filling < 0.5)
        {
            return this.BaseToll + 2.00m;
        }

        if (filling > 0.9)
        {
            return this.BaseToll - 1.00m;
        }

        return this.BaseToll;
    }
}
