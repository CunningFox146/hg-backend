using Microsoft.AspNetCore.Mvc;
using TheHungerGames.Systems.PlayerRating;

namespace TheHungerGames.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingController : Controller
{
    private readonly IPlayerRating _playerRating;

    public RatingController(IPlayerRating playerRating)
    {
        _playerRating = playerRating;
    }

    [HttpGet(Name = nameof(GetRating))]
    public async Task<double> GetRating(string playerId)
    {
        return await _playerRating.GetPlayerRating(playerId);
    }
}