namespace TheHungerGames.Systems.Rollback;

public interface IRollbackSystem
{
    Task Rollback(string sessionId, DateTime targetTime);
}