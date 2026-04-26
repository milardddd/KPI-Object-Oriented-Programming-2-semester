using CountdownTimer.EventArgsClasses;
using CountdownTimer.Interfaces;

namespace CountdownTimer.Implementation;

public class CountdownNotifier : ICountdownNotifier
{
    private readonly Timer timer;
    private EventHandler<StartedEventArgs>? startHandler;
    private EventHandler<StoppedEventArgs>? stopHandler;
    private EventHandler<TickEventArgs>? tickHandler;

    public CountdownNotifier(Timer timer)
    {
        this.timer = timer ?? throw new ArgumentNullException(nameof(timer));
    }

    public void Init(EventHandler<StartedEventArgs>? startHandler, EventHandler<StoppedEventArgs>? stopHandler, EventHandler<TickEventArgs>? tickHandler)
    {
        if (this.startHandler is not null)
        {
            this.timer.Started -= this.startHandler;
        }

        if (this.stopHandler is not null)
        {
            this.timer.Stopped -= this.stopHandler;
        }

        if (this.tickHandler is not null)
        {
            this.timer.Tick -= this.tickHandler;
        }

        this.startHandler = startHandler;
        this.stopHandler = stopHandler;
        this.tickHandler = tickHandler;

        if (this.startHandler is not null)
        {
            this.timer.Started += this.startHandler;
        }

        if (this.stopHandler is not null)
        {
            this.timer.Stopped += this.stopHandler;
        }

        if (this.tickHandler is not null)
        {
            this.timer.Tick += this.tickHandler;
        }
    }

    public void Run()
    {
        this.timer.Run();
    }
}
