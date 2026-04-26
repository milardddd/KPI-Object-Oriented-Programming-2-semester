namespace GenericSequenceGenerator;

public abstract class SequenceGenerator<T> : ISequenceGenerator<T>
{
    private T previous;

    private T current;

    protected SequenceGenerator(T previous, T current)
    {
        this.previous = previous;
        this.current = current;
        this.Count = 2;
    }

    public T Previous => this.previous;

    public T Current => this.current;

    public T Next
    {
        get
        {
            T next = this.GetNext();
            this.previous = this.current;
            this.current = next;
            this.Count++;
            return next;
        }
    }

    public int Count { get; private set; }

    protected abstract T GetNext();
}
