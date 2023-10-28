using Microsoft.EntityFrameworkCore;
using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class GameEndHandler : IAnalyticsEventHandler
{
    public AnalyticsEventType AnalyticsEvent => AnalyticsEventType.GameEnd;
    private readonly DataContext _dataContext;

    public GameEndHandler(DataContext context)
    {
        _dataContext = context;
    }

    public async Task Handle(AnalyticsEvent analyticsEvent)
    {
        var game = await _dataContext.Games.FirstOrDefaultAsync(g => g.SessionId == analyticsEvent.SessionId);
        if (game is not null)
        {
            game.GameEnd = DateTime.UtcNow;
            _dataContext.Games.Update(game);
        }

        await _dataContext.SaveChangesAsync();
    }
}