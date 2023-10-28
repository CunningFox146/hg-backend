using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class CharacterPickHandler : AnalyticsHandler<CharacterPick>
{
    public override AnalyticsEventType AnalyticsEvent => AnalyticsEventType.CharacterPick;
    
    public CharacterPickHandler(DataContext context) : base(context)
    {
    }

    protected override async Task Handle(CharacterPick data)
    {
        data.Id = Guid.NewGuid();
        await dataContext.CharacterPicks.AddAsync(data);
        await dataContext.SaveChangesAsync();
    }
}