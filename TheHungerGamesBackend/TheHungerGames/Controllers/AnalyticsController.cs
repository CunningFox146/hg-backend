using Microsoft.AspNetCore.Mvc;
using TheHungerGames.ModelProviders;
using TheHungerGames.Models;
using TheHungerGames.Systems.Analytics;

namespace TheHungerGames.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : Controller
{
    private readonly IAnalyticsSystem _analyticsSystem;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(IAnalyticsSystem analyticsSystem, ILogger<AnalyticsController> logger)
    {
        _analyticsSystem = analyticsSystem;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> HandleEvents(
        [ModelBinder(typeof(KleiJsonModelBinder))]
        List<AnalyticsEvent> events)
    {
        try
        {
            foreach (var analyticsEvent in events.OrderBy(e => e.Registered))
                await _analyticsSystem.HandleEvent(analyticsEvent);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}