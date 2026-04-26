using System;

namespace Interfaces;

public abstract class Deposit : IComparable<Deposit>
{
    protected Deposit(decimal depositAmount, int depositPeriod)
    {
        this.Amount = depositAmount;
        this.Period = depositPeriod;
    }

    public decimal Amount { get; }

    public int Period { get; }

    public static bool operator ==(Deposit left, Deposit right) =>
        ReferenceEquals(left, right) || (left is not null && left.CompareTo(right) == 0);

    public static bool operator !=(Deposit left, Deposit right) => !(left == right);

    public static bool operator <(Deposit left, Deposit right) => left?.CompareTo(right) < 0;

    public static bool operator >(Deposit left, Deposit right) => left?.CompareTo(right) > 0;

    public static bool operator <=(Deposit left, Deposit right) => left?.CompareTo(right) <= 0;

    public static bool operator >=(Deposit left, Deposit right) => left?.CompareTo(right) >= 0;

    public abstract decimal Income();

    public int CompareTo(Deposit other)
    {
        if (other is null)
        {
            return 1;
        }

        decimal thisTotal = this.Amount + this.Income();
        decimal otherTotal = other.Amount + other.Income();

        return thisTotal.CompareTo(otherTotal);
    }

    public override bool Equals(object obj) => obj is Deposit other && this.CompareTo(other) == 0;

    public override int GetHashCode() => HashCode.Combine(this.Amount, this.Period);
}