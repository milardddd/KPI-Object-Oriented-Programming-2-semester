namespace GenericSequenceGenerator;

public class CharSequenceGenerator : SequenceGenerator<char>
{
    public CharSequenceGenerator(char previous, char current)
        : base(previous, current)
    {
    }

    protected override char GetNext()
    {
        int next = ((this.Current + this.Previous) % 26) + 'A';
        return (char)next;
    }
}
