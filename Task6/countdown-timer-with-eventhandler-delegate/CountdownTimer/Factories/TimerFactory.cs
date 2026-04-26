namespace CountdownTimer.Factories;

public class TimerFactory
{
    public Timer CreateTimer(string name, int ticks)
    {
        return new Timer(name, ticks);
    }
}
