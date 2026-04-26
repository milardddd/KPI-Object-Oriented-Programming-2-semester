namespace GenericSequenceGenerator;

public class FibonacciSequenceGenerator : SequenceGenerator<int>
{
    public FibonacciSequenceGenerator(int previous, int current)
        : base(previous, current)
    {
    }

    protected override int GetNext() => this.Previous + this.Current;
}
