using CustomTimer.Interfaces;

namespace CustomTimer.Factories;

public class CountDownNotifierFactory
{
#pragma warning disable CA1822
    public ICountDownNotifier CreateNotifierForTimer(Timer timer)
    {
        return new Implementation.CountDownNotifier(timer);
    }
#pragma warning restore CA1822
}
