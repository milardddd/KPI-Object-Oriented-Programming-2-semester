using System;
using System.Globalization;
using BankSystem.Helpers;

namespace BankSystem.Generators;

public class BasedOnTickUniqueNumberGenerator : IUniqueNumberGenerator
{
    private readonly DateTime startingPoint;

    public BasedOnTickUniqueNumberGenerator(DateTime startingPoint)
    {
        this.startingPoint = startingPoint;
    }

    public string Generate()
    {
        long elapsedTicks = DateTime.UtcNow.Ticks - this.startingPoint.Ticks;
        return elapsedTicks.ToString(CultureInfo.InvariantCulture).GenerateHash();
    }
}
