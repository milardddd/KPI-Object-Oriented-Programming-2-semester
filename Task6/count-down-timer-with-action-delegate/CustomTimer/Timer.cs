namespace CustomTimer;

public class Timer
{
    private readonly string name;
    private readonly int ticks;

    public Timer(string name, int ticks)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Timer name cannot be null or empty.", nameof(name));
        }

        if (ticks <= 0)
        {
            throw new ArgumentException("Ticks count must be greater than zero.", nameof(ticks));
        }

        this.name = name;
        this.ticks = ticks;
    }

#pragma warning disable CA1003
    public event Action<string, int>? Started;

    public event Action<string, int>? Tick;

    public event Action<string>? Stopped;
#pragma warning restore CA1003

    public void Run()
    {
        this.Started?.Invoke(this.name, this.ticks);

        for (int remains = this.ticks - 1; remains >= 1; remains--)
        {
            Thread.Sleep(10);
            this.Tick?.Invoke(this.name, remains);
        }

        this.Stopped?.Invoke(this.name);
    }
}
