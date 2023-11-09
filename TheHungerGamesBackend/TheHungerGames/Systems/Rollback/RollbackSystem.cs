using System.Collections;
using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.Rollback;

public class RollbackSystem : IRollbackSystem
{
    private readonly ILogger<RollbackSystem> _logger;
    private readonly IEnumerable<IRollbackable> _rollbackables;
    private readonly DataContext _dataContext;
    
    public RollbackSystem(ILogger<RollbackSystem> logger, DataContext dataContext, IEnumerable<IRollbackable> rollbackables)
    {
        _logger = logger;
        _dataContext = dataContext;
        _rollbackables = rollbackables;

    }

    public async Task Rollback(string sessionId, DateTime targetTime)
    {
        _logger.LogInformation($"Rolling back game {sessionId} to {targetTime}");

        foreach (var rollbackable in _rollbackables)
        {
            _logger.LogInformation($"Rolling back {rollbackable.GetType().Name}");
            await rollbackable.Rollback(sessionId, targetTime);
        }
        
        await _dataContext.SaveChangesAsync();
    }
}