using Microsoft.EntityFrameworkCore;
using TheHungerGames.Data;
using TheHungerGames.Models;
using TheHungerGames.Systems.Rollback;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class GameLaunchHandler : AnalyticsHandler<GameModel>
{
    private readonly IRollbackSystem _rollbackSystem;

    public GameLaunchHandler(DataContext context, IRollbackSystem rollbackSystem) : base(context)
    {
        _rollbackSystem = rollbackSystem;
    }

    public override AnalyticsEventType AnalyticsEvent => AnalyticsEventType.GameLaunch;

    protected override async Task Handle(GameModel data)
    {
        if (!await dataContext.Games.AnyAsync(g => g.SessionId == data.SessionId))
            await AddNewGame(data);
        else
            await OnGameCrash();
    }

    private async Task AddNewGame(GameModel game)
    {
        await dataContext.Games.AddAsync(game);
        await dataContext.SaveChangesAsync();
    }

    private async Task OnGameCrash()
    {
        var game = await dataContext.Games.FirstAsync(game => game.SessionId == Source.SessionId);
        var lastSaveDate = game.LastSave ?? game.GameStart;
        await _rollbackSystem.Rollback(Source.SessionId, lastSaveDate);
    }
}