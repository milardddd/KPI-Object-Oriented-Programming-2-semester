namespace GenericSequenceGenerator;

public class DelegateSequenceGenerator<T> : SequenceGenerator<T>
{
    private readonly Func<T, T, T> generator;

    public DelegateSequenceGenerator(T previous, T current, Func<T, T, T> generator)
        : base(previous, current)
    {
        this.generator = generator ?? throw new ArgumentNullException(nameof(generator));
    }

    protected override T GetNext() => this.generator(this.Previous, this.Current);
}
