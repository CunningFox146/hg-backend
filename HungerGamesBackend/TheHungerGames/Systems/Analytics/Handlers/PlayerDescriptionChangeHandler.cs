using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class PlayerDescriptionChangeHandler : PlayerPropertyChangeHandler
{
    public override AnalyticsEventType AnalyticsEvent => AnalyticsEventType.PlayerDescriptionChange;
    
    public PlayerDescriptionChangeHandler(DataContext dataContext) : base(dataContext)
    {
    }

    protected override void UpdatePlayerProperty(Player player, Dictionary<string, string> data)
    {
        player.Description = data[nameof(player.Description)];
    }
}