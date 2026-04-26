using System.Globalization;
using BankSystem.Helpers;

namespace BankSystem.Generators;

public sealed class SimpleGenerator : IUniqueNumberGenerator
{
    private static readonly SimpleGenerator InstanceValue = new SimpleGenerator();
    private int lastNumber = 1234567890;

    private SimpleGenerator()
    {
    }

    public static SimpleGenerator Instance => InstanceValue;

    public string Generate()
    {
        this.lastNumber++;
        return this.lastNumber.ToString(CultureInfo.InvariantCulture).GenerateHash();
    }
}
