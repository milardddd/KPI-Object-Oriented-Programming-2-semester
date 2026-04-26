namespace GenericSequenceGenerator;

public class DoubleSequenceGenerator : SequenceGenerator<double>
{
    public DoubleSequenceGenerator(double previous, double current)
        : base(previous, current)
    {
    }

    protected override double GetNext() => this.Current + (this.Previous / this.Current);
}
