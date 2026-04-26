using System;

namespace TollCalculator;

public abstract class Vehicle
{
    private decimal baseToll;

    protected Vehicle()
    {
    }

    protected Vehicle(decimal baseToll)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(baseToll);
        this.baseToll = baseToll;
    }

    public decimal BaseToll
    {
        get => this.baseToll;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            this.baseToll = value;
        }
    }

    public decimal CalculateToll(DateTime timeOfToll, TrafficDirection direction)
    {
        return this.Calculate() * PeakTimePremium(timeOfToll, direction);
    }

    protected abstract decimal Calculate();

    private static decimal PeakTimePremium(DateTime timeOfToll, TrafficDirection direction)
    {
        if (!IsWeekDay(timeOfToll))
        {
            return 1.00m;
        }

        var timeBand = GetTimeBand(timeOfToll);

        return (timeBand, direction) switch
        {
            (TimeBand.MorningRush, TrafficDirection.Inbound) => 2.00m,
            (TimeBand.MorningRush, TrafficDirection.Outbound) => 1.00m,
            (TimeBand.Daytime, _) => 1.50m,
            (TimeBand.EveningRush, TrafficDirection.Inbound) => 1.00m,
            (TimeBand.EveningRush, TrafficDirection.Outbound) => 2.00m,
            (TimeBand.Overnight, _) => 0.75m,
            _ => 1.00m,
        };
    }

    private static bool IsWeekDay(DateTime timeOfToll)
    {
        return timeOfToll.DayOfWeek != DayOfWeek.Saturday && timeOfToll.DayOfWeek != DayOfWeek.Sunday;
    }

    private static TimeBand GetTimeBand(DateTime timeOfToll)
    {
        TimeSpan time = timeOfToll.TimeOfDay;

        if (time > TimeSpan.FromHours(6) && time <= TimeSpan.FromHours(10))
        {
            return TimeBand.MorningRush;
        }

        if (time > TimeSpan.FromHours(10) && time <= TimeSpan.FromHours(16))
        {
            return TimeBand.Daytime;
        }

        if (time > TimeSpan.FromHours(16) && time <= TimeSpan.FromHours(19))
        {
            return TimeBand.EveningRush;
        }

        return TimeBand.Overnight;
    }
}
