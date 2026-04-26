using System.Globalization;

namespace BookStoreCatalog;

public class BookPublication
{
    public BookPublication(string authorName, string isniCode, string title, string publisher, DateTime published, BookBindingKind bookBinding, string isbnCode)
        : this(new BookAuthor(authorName, isniCode), title, publisher, published, bookBinding, new BookNumber(isbnCode))
    {
    }

    public BookPublication(string authorName, string title, string publisher, DateTime published, BookBindingKind bookBinding, string isbnCode)
    : this(new BookAuthor(authorName), title, publisher, published, bookBinding, new BookNumber(isbnCode))
    {
    }

    public BookPublication(BookAuthor author, string title, string publisher, DateTime published, BookBindingKind bookBinding, BookNumber isbn)
    {
        this.Author = author ?? throw new ArgumentNullException(nameof(author));
        this.Isbn = isbn ?? throw new ArgumentNullException(nameof(isbn));

        ArgumentNullException.ThrowIfNull(title);

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Error", nameof(title));
        }

        ArgumentNullException.ThrowIfNull(publisher);

        if (string.IsNullOrWhiteSpace(publisher))
        {
            throw new ArgumentException("Error", nameof(publisher));
        }

        this.Title = title;
        this.Publisher = publisher;
        this.Published = published;
        this.BookBinding = bookBinding;
    }

    public BookAuthor Author { get; init; }

    public string Title { get; init; }

    public string Publisher { get; init; }

    public DateTime Published { get; init; }

    public BookBindingKind BookBinding { get; init; }

    public BookNumber Isbn { get; init; }

    public string GetPublicationDateString()
    {
        return this.Published.ToString("MMMM, yyy", CultureInfo.InvariantCulture);
    }

    public override string ToString()
    {
        return $"{this.Title} by {this.Author.AuthorName}";
    }
}
