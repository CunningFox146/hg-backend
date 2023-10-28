using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class PlayerCrashHandler : AnalyticsHandler<PlayerCrash>
{
    public PlayerCrashHandler(DataContext context) : base(context)
    {
    }

    public override AnalyticsEventType AnalyticsEvent => AnalyticsEventType.PlayerCrash;

    protected override async Task Handle(PlayerCrash data)
    {
        data.SessionId = Source.SessionId;
        await dataContext.PlayerCrashes.AddAsync(data);
        await dataContext.SaveChangesAsync();
    }
}