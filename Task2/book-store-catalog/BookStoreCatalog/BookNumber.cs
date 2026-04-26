namespace BookStoreCatalog;

public class BookNumber
{
    private readonly string code;

    public BookNumber(string isbnCode)
    {
        ArgumentNullException.ThrowIfNull(isbnCode);

        if (!ValidateCode(isbnCode) || !ValidateChecksum(isbnCode))
        {
            throw new ArgumentException("Invalid ISBN code.", nameof(isbnCode));
        }

        this.code = isbnCode;
    }

    public string Code => this.code;

    public Uri GetSearchUri()
    {
        return new Uri($"https://isbnsearch.org/isbn/{this.code}");
    }

    public override string ToString() => this.code;

    private static bool ValidateCode(string isbnCode)
    {
        if (isbnCode == null)
        {
            return false;
        }

        if (isbnCode.Length != 10)
        {
            return false;
        }

        for (int i = 0; i < 10; i++)
        {
            char c = isbnCode[i];

            if (i < 9 && !char.IsDigit(c))
            {
                return false;
            }

            if (i == 9 && !(char.IsDigit(c) || c == 'X'))
            {
                return false;
            }
        }

        return true;
    }

    private static bool ValidateChecksum(string isbnCode)
    {
        int sum = 0;

        for (int i = 0; i < 10; i++)
        {
            int val;
            if (isbnCode[i] == 'X')
            {
                val = 10;
            }
            else
            {
                val = isbnCode[i] - '0';
            }

            sum += (10 - i) * val;
        }

        return sum % 11 == 0;
    }
}
