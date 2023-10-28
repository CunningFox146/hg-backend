using TheHungerGames.Models;
using TheHungerGames.Systems.Analytics.Handlers;

namespace TheHungerGames.Systems.Analytics;

public class AnalyticsSystem : IAnalyticsSystem
{
    private readonly IEnumerable<IAnalyticsEventHandler> _handlers;
    private readonly ILogger<AnalyticsSystem> _logger;

    public AnalyticsSystem(IEnumerable<IAnalyticsEventHandler> handlers, ILogger<AnalyticsSystem> logger)
    {
        _handlers = handlers;
        _logger = logger;
    }

    public async Task HandleEvent(AnalyticsEvent @event)
    {
        var handler = _handlers.FirstOrDefault(handler => handler.AnalyticsEvent == @event.EventType);
        if (handler is null)
            throw new Exception($"No event handler for {@event.EventType.ToString()}");

        await handler.Handle(@event);
        _logger.Log(LogLevel.Information, $"{@event.EventType.ToString()} handled successfully");
    }
}