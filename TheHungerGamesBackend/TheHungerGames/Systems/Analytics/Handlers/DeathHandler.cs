using TheHungerGames.Data;
using TheHungerGames.Models;
using TheHungerGames.Systems.PlayerRating;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class DeathHandler : AnalyticsHandler<PlayerDeath>
{
    private readonly IPlayerRating _playerRating;

    public DeathHandler(DataContext context, IPlayerRating playerRating) : base(context)
    {
        _playerRating = playerRating;
    }

    public override AnalyticsEventType AnalyticsEvent => AnalyticsEventType.PlayerDeath;

    protected override async Task Handle(PlayerDeath data)
    {
        await dataContext.Deaths.AddAsync(data);
        await _playerRating.OnPlayerDeath(data.PlayerId, data.Killer, Source.SessionId);
        await dataContext.SaveChangesAsync();
    }
}