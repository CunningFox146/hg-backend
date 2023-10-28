using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class ArenaHandler : AnalyticsHandler<List<string>>
{
    public override AnalyticsEventType AnalyticsEvent => AnalyticsEventType.ArenaStart;

    public ArenaHandler(DataContext context) : base(context)
    {
    }

    protected override async Task Handle(List<string> data)
    {
        foreach (var playerId in data)
        {
            await dataContext.ArenaSurvivors.AddAsync(new ArenaSurvivor
            {
                Id = Guid.NewGuid(),
                PlayerId = playerId,
                SessionId = Source.SessionId,
                Registered = DateTime.UtcNow
            });
        }

        await dataContext.SaveChangesAsync();
    }
}