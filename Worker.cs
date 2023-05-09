namespace DayTimeCoffee;

public class Worker : BackgroundService
{
    private readonly PeriodicTimer _timer;
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int i = 0;
        Console.WriteLine("Drinking coffee ");
        while (!stoppingToken.IsCancellationRequested)
        {
            await _timer.WaitForNextTickAsync(stoppingToken).ConfigureAwait(false);

            DateTime localNow = DateTime.Now;
            DateTime start = DateTime.Today.AddHours(6); // 6am
            DateTime end = DateTime.Today.AddHours(23); // 11pm
            // During the day
            if (localNow >= start && localNow <= end)
            {
                bool result = Native.NoSleep();
                _logger.LogTrace("Drink some coffee. Result: {result}", result);
            }
            else
            {
                bool result = Native.OkayToSleep();
                _logger.LogTrace("Take some sleep. Result: {result}", result);
            }

            if (i++ >= 1000)
            {
                i = 0;
            }
            if (i == 1)
            {
                Console.Write(".");
            }
            if (i % 20 == 0)
            {
                Console.WriteLine();
            }
        }
    }
}
