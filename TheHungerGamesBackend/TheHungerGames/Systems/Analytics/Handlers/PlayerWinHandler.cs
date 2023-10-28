using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class PlayerWinHandler : AnalyticsHandler<PlayerWin>
{
    public PlayerWinHandler(DataContext context) : base(context)
    {
    }

    public override AnalyticsEventType AnalyticsEvent => AnalyticsEventType.PlayerWin;

    protected override async Task Handle(PlayerWin data)
    {
        await dataContext.Wins.AddAsync(data);
        await dataContext.SaveChangesAsync();
    }
}