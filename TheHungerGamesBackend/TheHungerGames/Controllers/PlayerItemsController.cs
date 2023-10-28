using Microsoft.AspNetCore.Mvc;
using TheHungerGames.Systems.PlayerItems;

namespace TheHungerGames.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerItemsController : Controller
{
    private readonly IPlayerItemsSystem _playerItems;

    public PlayerItemsController(IPlayerItemsSystem playerItems)
    {
        _playerItems = playerItems;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddItems([FromQuery] string ownerId, [FromBody] List<string> items)
    {
        await _playerItems.AddItems(ownerId, items);
        return Ok();
    }
}