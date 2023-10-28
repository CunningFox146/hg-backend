using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public abstract class PlayerPropertyChangeHandler : IAnalyticsEventHandler
{
    private readonly DataContext _dataContext;
    public abstract AnalyticsEventType AnalyticsEvent { get; }

    protected PlayerPropertyChangeHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    protected abstract void UpdatePlayerProperty(Player player, Dictionary<string, string> data);
    
    public async Task Handle(AnalyticsEvent analyticsEvent)
    {
        var data = JsonSerializer.Deserialize<Dictionary<string, string>>(analyticsEvent.EventArgs, JsonSerializerOptions.Default);
        var playerId = data["PlayerId"];

        var player = await _dataContext.Players.FirstOrDefaultAsync(p => p.PlayerId == playerId);
        if (player is null)
            throw new Exception($"Failed to find player with id ${playerId}");

        UpdatePlayerProperty(player, data);
        
        _dataContext.Update(player);
        await _dataContext.SaveChangesAsync();
    }
}