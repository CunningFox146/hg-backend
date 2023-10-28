using Microsoft.EntityFrameworkCore;
using TheHungerGames.Data;
using TheHungerGames.Models;
using TheHungerGames.Systems.PlayerRating;

namespace TheHungerGames.Systems.Analytics.Handlers;

public class PlayerJoinHandler : AnalyticsHandler<PlayerJoin>
{
    private readonly IPlayerRating _playerRating;

    public PlayerJoinHandler(DataContext context, IPlayerRating playerRating) : base(context)
    {
        _playerRating = playerRating;
    }

    public override AnalyticsEventType AnalyticsEvent => AnalyticsEventType.PlayerJoin;

    protected override async Task Handle(PlayerJoin data)
    {
        await dataContext.PlayerJoins.AddAsync(data);
        if (!await dataContext.Players.AnyAsync(player => player.PlayerId == data.PlayerId))
            await AddNewPlayer(data);
        else
            await UpdatePlayer(data);
        await dataContext.SaveChangesAsync();
    }

    private async Task AddNewPlayer(PlayerJoin data)
    {
        var player = new Player
        {
            PlayerId = data.PlayerId,
            Name = data.Name,
            Steam = data.Steam
        };
        
        await dataContext.Players.AddAsync(player);
        await _playerRating.AddNewPlayer(player);
    }

    private async Task UpdatePlayer(PlayerJoin data)
    {
        var player = await dataContext.Players.FirstAsync(p => p.PlayerId == data.PlayerId);
        player.Name = data.Name;
        player.Steam = data.Steam;
        dataContext.Players.Update(player);
    }
}