using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics;

public interface IAnalyticsSystem
{
    Task HandleEvent(AnalyticsEvent @event);
}