using Microsoft.EntityFrameworkCore;
using TheHungerGames.Data;
using TheHungerGames.Models;
using TheHungerGames.Systems.Rollback;
using TheHungerGames.Util;

namespace TheHungerGames.Systems.PlayerRating;

public class EloRating : IPlayerRating, IRollbackable
{
    private readonly DataContext _dataContext;

    public EloRating(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<double> GetPlayerRating(string playerId)
    {
        return (await GetPlayerRatingData(playerId))?.Rating ?? EloRatingCalculator.StartRating;
    }

    public async Task OnPlayerDeath(string playerId, string killer, int sessionId)
    {
        var playerRating = await GetPlayerRatingData(playerId);
        var killerRating = killer.IsPlayerId() ? await GetPlayerRatingData(killer) : null;
        var isPlayerOnArena = await _dataContext.ArenaSurvivors.AnyAsync(s => s.SessionId == sessionId && s.Registered <= DateTime.UtcNow && s.PlayerId == playerId);

        var newRating = EloRatingCalculator.CalculateNewRatings(
            playerRating.Rating,
            killerRating?.Rating ?? playerRating.Rating,
            EloRatingCalculator.Win,
            EloRatingCalculator.Loose);

        if (!isPlayerOnArena)
            RecordRatingChange(playerRating, newRating.ratingA, sessionId);

        if (killerRating is not null)
            RecordRatingChange(killerRating, newRating.ratingB, sessionId);

        await _dataContext.SaveChangesAsync();
    }

    private async void RecordRatingChange(Models.PlayerRating playerRating, double newRating, int sessionId)
    {
        var ratingDelta = newRating - playerRating.Rating;
        
        if (Math.Abs(ratingDelta) > 0)
        {
            var delta = new RatingDelta
            {
                Id = Guid.NewGuid(),
                Registered = DateTime.Now,
                SessionId = sessionId,
                PlayerId = playerRating.PlayerId,
                Value = ratingDelta
            };
            await _dataContext.RatingDelta.AddAsync(delta);
        }

        playerRating.Rating = newRating;
        _dataContext.PlayerRating.Update(playerRating);
    }

    public async Task AddNewPlayer(Player player)
    {
        await _dataContext.PlayerRating.AddAsync(new Models.PlayerRating()
        {
            PlayerId = player.PlayerId,
            Rating = EloRatingCalculator.StartRating,
            Season = 0 // TODO: add seasons system
        });
    }

    private async Task<Models.PlayerRating> GetPlayerRatingData(string playerId)
    {
        if (playerId is null || !playerId.IsPlayerId())
            throw new Exception($"Invalid Player Id: {playerId}");

        return await _dataContext.PlayerRating.FirstOrDefaultAsync(player => player.PlayerId == playerId);
    }

    public async Task Rollback(int sessionId, DateTime targetTime)
    {
        var rating = _dataContext.RatingDelta.Where(d => d.SessionId == sessionId && d.Registered >= targetTime);
        foreach (var ratingDelta in rating)
        {
            var player = await _dataContext.PlayerRating.FirstOrDefaultAsync(p => p.PlayerId == ratingDelta.PlayerId);
            player.Rating = Math.Max(player.Rating - ratingDelta.Value, EloRatingCalculator.StartRating);
        }

        foreach (var ratingDelta in rating)
        {
            _dataContext.RatingDelta.Remove(ratingDelta);
        }
    }
}