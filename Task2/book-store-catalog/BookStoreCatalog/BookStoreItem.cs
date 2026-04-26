namespace BookStoreCatalog;

public class BookStoreItem
{
    private BookPublication publication;

    private BookPrice price;

    private int amount;

    public BookStoreItem(string authorName, string isniCode, string title, string publisher, DateTime published, BookBindingKind bookBinding, string isbn, decimal priceAmount, string priceCurrency, int amount)
    : this(new BookPublication(authorName, isniCode, title, publisher, published, bookBinding, isbn), new BookPrice(priceAmount, priceCurrency), amount)
    {
    }

    public BookStoreItem(BookPublication publication, BookPrice price, int amount)
    {
        ArgumentNullException.ThrowIfNull(publication);
        ArgumentNullException.ThrowIfNull(price);

        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative");
        }

        this.publication = publication;
        this.price = price;
        this.amount = amount;
    }

    public BookPublication Publication
    {
        get => this.publication;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            this.publication = value;
        }
    }

    public BookPrice Price
    {
        get => this.price;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            this.price = value;
        }
    }

    public int Amount
    {
        get => this.amount;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Amount cannot be negative");
            }

            this.amount = value;
        }
    }

    public override string ToString()
    {
        string pricePart = this.Price.ToString();

        if (pricePart.Contains(',', StringComparison.Ordinal))
        {
            pricePart = $"\"{pricePart}\"";
        }

        if (!this.Publication.Author.HasIsni)
        {
            return $"{this.Publication}, {pricePart}, {this.Amount}";
        }
        else
        {
            return $"{this.Publication.Title} by {this.Publication.Author}, {pricePart}, {this.Amount}";
        }
    }
}
