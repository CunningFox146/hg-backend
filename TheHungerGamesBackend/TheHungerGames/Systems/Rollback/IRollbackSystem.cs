namespace TheHungerGames.Systems.Rollback;

public interface IRollbackSystem
{
    Task Rollback(int sessionId, DateTime targetTime);
}