using CustomTimer.Factories;
using NUnit.Framework;

namespace CustomTimer.Tests;

public class CountDownNotifierTests
{
    private CountDownNotifierFactory countDownNotifierFactory = null!;
    private TimerFactory timerFactory = null!;

    [SetUp]
    public void Setup()
    {
        this.countDownNotifierFactory = new CountDownNotifierFactory();
        this.timerFactory = new TimerFactory();
    }

    [TestCase("pie", 10)]
    [TestCase("cookies", 5)]
    [TestCase("pizza", 1)]
    public void Run_ValidTimer_AllEventsWorkAsExpected(string name, int totalTicks)
    {
        var timer = this.timerFactory.CreateTimer(name, totalTicks);
        var notifier = this.countDownNotifierFactory.CreateNotifierForTimer(timer);

        void TimerStarted(string timerName, int ticks)
        {
            Assert.That(name, Is.EqualTo(timerName));
            Assert.That(totalTicks, Is.EqualTo(ticks));
            Console.WriteLine($"Start timer '{timerName}', total {ticks} ticks");
        }

        void TimerStopped(string timerName)
        {
            Assert.That(name, Is.EqualTo(timerName));
            Console.WriteLine($"Stop timer '{timerName}'");
        }

        var remainsTicks = totalTicks;

        void TimerTick(string timerName, int ticks)
        {
            remainsTicks -= 1;
            Assert.That(name, Is.EqualTo(timerName));
            Assert.That(remainsTicks, Is.EqualTo(ticks));
            Console.WriteLine($"Timer '{timerName}', remains {ticks} ticks");
        }

        notifier.Init(TimerStarted, TimerStopped, TimerTick);
        notifier.Run();

        Assert.That(0, Is.EqualTo(remainsTicks - 1));
    }

    [TestCase("pie", 10)]
    [TestCase("cookies", 5)]
    [TestCase("pizza", 1)]
    public void Run_NullDelegates_TimerIsWorking(string name, int totalTicks)
    {
        var timer = this.timerFactory.CreateTimer(name, totalTicks);
        var notifier = this.countDownNotifierFactory.CreateNotifierForTimer(timer);

        Assert.DoesNotThrow(() =>
        {
            notifier.Init(null!, null!, null!);
            notifier.Run();
        });
    }

    [Test]
    public void Ctor_TimerIsNull_ThrowsArgumentNullException()
        => Assert.Throws<ArgumentNullException>(() => this.countDownNotifierFactory.CreateNotifierForTimer(null!));
}
