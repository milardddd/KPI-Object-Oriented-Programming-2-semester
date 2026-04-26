namespace BookStoreCatalog;

public class NameIdentifier
{
    public NameIdentifier(string isniCode)
    {
        ArgumentNullException.ThrowIfNull(isniCode);

        if (!ValidateCode(isniCode))
        {
            throw new ArgumentException("Invalid ISNI code.", nameof(isniCode));
        }

        this.Code = isniCode;
    }

    public string Code { get; init; }

    public Uri GetUri()
    {
        return new Uri("https://isni.org/isni/" + this.Code);
    }

    public override string ToString() => this.Code;

    private static bool ValidateCode(string isniCode)
    {
        if (isniCode == null)
        {
            return false;
        }

        if (isniCode.Length != 16)
        {
            return false;
        }

        return isniCode.All(c => char.IsDigit(c) || c == 'X');
    }
}
