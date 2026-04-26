using System;

namespace TollCalculator;

public class Taxi : Vehicle
{
    private int passengers;

    public Taxi(decimal baseToll, int passengers)
        : base(baseToll)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(passengers);
        this.passengers = passengers;
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
        return this.BaseToll + this.Passengers switch
        {
            0 => 0.50m,
            2 => -0.50m,
            >= 3 => -1.00m,
            _ => 0m,
        };
    }
}
