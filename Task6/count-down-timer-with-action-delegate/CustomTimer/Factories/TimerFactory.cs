namespace CustomTimer.Factories;

public class TimerFactory
{
#pragma warning disable CA1822
    public Timer CreateTimer(string name, int ticks)
    {
        return new Timer(name, ticks);
    }
#pragma warning restore CA1822
}
