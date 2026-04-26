using CountdownTimer.EventArgsClasses;

namespace CountdownTimer;

public class Timer
{
    private const int TickDelayMilliseconds = 10;

    public Timer(string timerName, int ticks)
    {
        if (string.IsNullOrEmpty(timerName))
        {
            throw new ArgumentException("Timer name cannot be null or empty.", nameof(timerName));
        }

        if (ticks <= 0)
        {
            throw new ArgumentException("Ticks must be greater than zero.", nameof(ticks));
        }

        this.Name = timerName;
        this.Ticks = ticks;
    }

    public event EventHandler<StartedEventArgs>? Started;

    public event EventHandler<TickEventArgs>? Tick;

    public event EventHandler<StoppedEventArgs>? Stopped;

    public string Name { get; }

    public int Ticks { get; }

    public void Run()
    {
        this.OnStarted(new StartedEventArgs(this.Name, this.Ticks));

        for (var remainingTicks = this.Ticks - 1; remainingTicks >= 0; remainingTicks--)
        {
            Thread.Sleep(TickDelayMilliseconds);
            this.OnTick(new TickEventArgs(this.Name, remainingTicks));
        }

        this.OnStopped(new StoppedEventArgs(this.Name));
    }

    protected virtual void OnStarted(StartedEventArgs e)
    {
        this.Started?.Invoke(this, e);
    }

    protected virtual void OnTick(TickEventArgs e)
    {
        this.Tick?.Invoke(this, e);
    }

    protected virtual void OnStopped(StoppedEventArgs e)
    {
        this.Stopped?.Invoke(this, e);
    }
}
