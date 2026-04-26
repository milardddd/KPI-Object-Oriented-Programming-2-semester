namespace BookStoreCatalog;

public class BookAuthor
{
    public BookAuthor(string authorName)
    {
        ArgumentNullException.ThrowIfNull(authorName);

        if (string.IsNullOrWhiteSpace(authorName))
        {
            throw new ArgumentException("Author name cannot be empty.", nameof(authorName));
        }

        this.AuthorName = authorName;
        this.HasIsni = false;
    }

    public BookAuthor(string authorName, string isniCode)
        : this(authorName, new NameIdentifier(isniCode))
    {
    }

    public BookAuthor(string authorName, NameIdentifier nameIdentifier)
        : this(authorName)
    {
        ArgumentNullException.ThrowIfNull(nameIdentifier);

        this.HasIsni = true;
        this.Isni = nameIdentifier;
    }

    public string AuthorName { get; private set; }

    public bool HasIsni { get; private set; }

    public NameIdentifier Isni { get; private set; } = null!;

    public override string ToString()
    {
        return this.HasIsni ? $"{this.AuthorName} (ISNI:{this.Isni})" : this.AuthorName;
    }
}
