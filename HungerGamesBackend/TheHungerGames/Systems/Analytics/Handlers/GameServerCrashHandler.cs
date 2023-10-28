using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class GameServerCrashHandler : AnalyticsHandler<GameServerCrash>
{
    public GameServerCrashHandler(DataContext context) : base(context)
    {
    }

    public override AnalyticsEventType AnalyticsEvent => AnalyticsEventType.GameServerCrash;


    protected override async Task Handle(GameServerCrash data)
    {
        await dataContext.GameServerCrashes.AddAsync(data);
        await dataContext.SaveChangesAsync();
    }
}