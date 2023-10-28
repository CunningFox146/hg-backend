using Microsoft.EntityFrameworkCore;
using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class GameSaveHandler : IAnalyticsEventHandler
{
    private readonly DataContext _dataContext;

    public GameSaveHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public AnalyticsEventType AnalyticsEvent => AnalyticsEventType.GameSave;

    public async Task Handle(AnalyticsEvent analyticsEvent)
    {
        var game = await _dataContext.Games.FirstOrDefaultAsync(game => game.SessionId == analyticsEvent.SessionId);
        if (game is null)
            throw new Exception($"Failed to find game with id {analyticsEvent.SessionId}");

        game.LastSave = DateTime.Now;
        await _dataContext.SaveChangesAsync();
    }
}