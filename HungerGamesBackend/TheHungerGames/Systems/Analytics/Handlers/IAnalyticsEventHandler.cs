using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public interface IAnalyticsEventHandler
{
    AnalyticsEventType AnalyticsEvent { get; }
    Task Handle(AnalyticsEvent analyticsEvent);
}