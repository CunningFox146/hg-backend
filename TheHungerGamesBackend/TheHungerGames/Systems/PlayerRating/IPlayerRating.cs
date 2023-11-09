using TheHungerGames.Models;

namespace TheHungerGames.Systems.PlayerRating;

public interface IPlayerRating
{
    Task<double> GetPlayerRating(string playerId);
    Task OnPlayerDeath(string playerId, string killer, string sessionId);
    Task AddNewPlayer(Player player);
}