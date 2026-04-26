namespace BookStoreItem;

public class BookStoreItem
{
    private readonly string authorName;
    private readonly string? isni;
    private readonly bool hasIsni;
    private decimal price;
    private string currency = "USD";
    private int amount;

    public BookStoreItem(string authorName, string title, string publisher, string isbn)
        : this(authorName, null, title, publisher, isbn, null, string.Empty, 0m, "USD", 0)
    {
    }

    public BookStoreItem(string authorName, string isni, string title, string publisher, string isbn)
        : this(authorName, isni, title, publisher, isbn, null, string.Empty, 0m, "USD", 0)
    {
    }

    public BookStoreItem(string authorName, string title, string publisher, string isbn, DateTime? published, string bookBinding = "", decimal price = 0m, string currency = "USD", int amount = 0)
        : this(authorName, null, title, publisher, isbn, published, bookBinding, price, currency, amount)
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S107:Methods should not have too many parameters", Justification = "Public constructor signature is required by task tests.")]
    public BookStoreItem(string authorName, string? isni, string title, string publisher, string isbn, DateTime? published, string bookBinding = "", decimal price = 0m, string currency = "USD", int amount = 0)
    {
        if (string.IsNullOrWhiteSpace(authorName))
        {
            throw new ArgumentException("Value cannot be null, empty, or whitespace.", nameof(authorName));
        }

        if (!authorName.Any(char.IsLetter))
        {
            throw new ArgumentException("Value must contain at least one letter.", nameof(authorName));
        }

        if (isni is not null && !ValidateIsni(isni))
        {
            throw new ArgumentException("ISNI is not valid.", nameof(isni));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Value cannot be null, empty, or whitespace.", nameof(title));
        }

        if (!title.Any(char.IsLetter))
        {
            throw new ArgumentException("Value must contain at least one letter.", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(publisher))
        {
            throw new ArgumentException("Value cannot be null, empty, or whitespace.", nameof(publisher));
        }

        if (!publisher.Any(char.IsLetter))
        {
            throw new ArgumentException("Value must contain at least one letter.", nameof(publisher));
        }

        if (string.IsNullOrWhiteSpace(isbn) || !ValidateIsbnFormat(isbn) || !ValidateIsbnChecksum(isbn))
        {
            throw new ArgumentException("ISBN is not valid.", nameof(isbn));
        }

        ThrowExceptionIfCurrencyIsNotValid(currency);

        this.authorName = authorName;
        this.isni = isni;
        this.hasIsni = isni is not null;
        this.Title = title;
        this.Publisher = publisher;
        this.Isbn = isbn;
        this.Published = published;
        this.BookBinding = bookBinding;
        this.Price = price;
        this.Currency = currency;
        this.Amount = amount;
    }

    public string AuthorName => this.authorName;

    public string? Isni => this.isni;

    public bool HasIsni => this.hasIsni;

    public string Title { get; private set; }

    public string Publisher { get; private set; }

    public string Isbn { get; private set; }

    public DateTime? Published { get; set; }

    public string BookBinding { get; set; }

    public decimal Price
    {
        get
        {
            return this.price;
        }

        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be negative.");
            }

            this.price = value;
        }
    }

    public string Currency
    {
        get
        {
            return this.currency;
        }

        set
        {
            ThrowExceptionIfCurrencyIsNotValid(value);
            this.currency = value;
        }
    }

    public int Amount
    {
        get
        {
            return this.amount;
        }

        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Amount cannot be negative.");
            }

            this.amount = value;
        }
    }

    public Uri GetIsniUri()
    {
        if (!this.HasIsni || this.Isni is null)
        {
            throw new InvalidOperationException("ISNI is not set.");
        }

        return new Uri($"https://isni.org/isni/{this.Isni}", UriKind.Absolute);
    }

    public Uri GetIsbnSearchUri()
    {
        return new Uri($"https://isbnsearch.org/isbn/{this.Isbn}", UriKind.Absolute);
    }

    public override string ToString()
    {
        string isniPart = this.HasIsni && this.Isni is not null ? this.Isni : "ISNI IS NOT SET";
        string pricePart = this.Price.ToString("N2", System.Globalization.CultureInfo.InvariantCulture);
        string priceWithCurrency = $"{pricePart} {this.Currency}";

        if (pricePart.Contains(',', StringComparison.Ordinal))
        {
            priceWithCurrency = $"\"{priceWithCurrency}\"";
        }

        return $"{this.Title}, {this.AuthorName}, {isniPart}, {priceWithCurrency}, {this.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
    }

    private static bool ValidateIsni(string isni)
    {
        return isni.Length == 16 && isni.All(c => char.IsDigit(c) || c == 'X');
    }

    private static bool ValidateIsbnFormat(string isbn)
    {
        return isbn.Length == 10 && isbn.All(c => char.IsDigit(c) || c == 'X');
    }

    private static bool ValidateIsbnChecksum(string isbn)
    {
        if (!ValidateIsbnFormat(isbn))
        {
            return false;
        }

        int checksum = 0;
        for (int i = 0; i < isbn.Length; i++)
        {
            int digit = isbn[i] == 'X' ? 10 : isbn[i] - '0';
            checksum += (10 - i) * digit;
        }

        return checksum % 11 == 0;
    }

    private static void ThrowExceptionIfCurrencyIsNotValid(string currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3 || !currency.All(char.IsLetter))
        {
            throw new ArgumentException("Currency is not valid.", nameof(currency));
        }
    }
}
