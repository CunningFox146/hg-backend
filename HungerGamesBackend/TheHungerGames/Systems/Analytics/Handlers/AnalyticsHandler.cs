using Newtonsoft.Json;
using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public abstract class AnalyticsHandler<T> : IAnalyticsEventHandler
{
    protected readonly DataContext dataContext;

    protected AnalyticsHandler(DataContext context)
    {
        dataContext = context;
    }

    protected AnalyticsEvent Source { get; private set; }
    public abstract AnalyticsEventType AnalyticsEvent { get; }

    public Task Handle(AnalyticsEvent analyticsEvent)
    {
        if (string.IsNullOrEmpty(analyticsEvent.EventArgs))
            throw new Exception("EventArgs cannot be null");
            
        var data = JsonConvert.DeserializeObject<T>(analyticsEvent.EventArgs);

        if (data is null)
            throw new JsonException($"Failed to decode {nameof(T)}");

        Source = analyticsEvent;

        if (data is IDateRegistered dateRegistered)
            dateRegistered.Registered = analyticsEvent.Registered ?? DateTime.UtcNow;

        if (data is ISessionRecorder sessionRecorder)
            sessionRecorder.SessionId = Source.SessionId;

        return Handle(data);
    }

    protected abstract Task Handle(T data);
}