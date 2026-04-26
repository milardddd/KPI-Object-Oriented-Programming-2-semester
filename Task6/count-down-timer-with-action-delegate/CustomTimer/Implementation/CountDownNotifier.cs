using CustomTimer.Interfaces;

namespace CustomTimer.Implementation;

public class CountDownNotifier : ICountDownNotifier
{
    private readonly Timer timer;

    public CountDownNotifier(Timer timer)
    {
        this.timer = timer ?? throw new ArgumentNullException(nameof(timer));
    }

    public void Init(Action<string, int> startHandler, Action<string> stopHandler, Action<string, int> tickHandler)
    {
        if (startHandler is not null)
        {
            this.timer.Started += startHandler;
        }

        if (stopHandler is not null)
        {
            this.timer.Stopped += stopHandler;
        }

        if (tickHandler is not null)
        {
            this.timer.Tick += tickHandler;
        }
    }

    public void Run()
    {
        this.timer.Run();
    }
}
