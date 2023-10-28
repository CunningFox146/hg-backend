using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class PlayerDiscordChangeHandler : PlayerPropertyChangeHandler
{
    public override AnalyticsEventType AnalyticsEvent => AnalyticsEventType.PlayerDiscordChange;
    
    public PlayerDiscordChangeHandler(DataContext dataContext) : base(dataContext)
    {
    }

    protected override void UpdatePlayerProperty(Player player, Dictionary<string, string> data)
    {
        player.Discord = data[nameof(player.Discord)];
    }
}