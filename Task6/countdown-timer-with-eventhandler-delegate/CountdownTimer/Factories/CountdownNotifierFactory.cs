using CountdownTimer.Implementation;
using CountdownTimer.Interfaces;

namespace CountdownTimer.Factories;

public class CountdownNotifierFactory
{
    public ICountdownNotifier CreateNotifierForTimer(Timer? timer)
    {
        return new CountdownNotifier(timer ?? throw new ArgumentNullException(nameof(timer)));
    }
}
