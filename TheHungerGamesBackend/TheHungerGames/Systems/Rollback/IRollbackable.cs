namespace TheHungerGames.Systems.Rollback;

public interface IRollbackable
{
    Task Rollback(string sessionId, DateTime targetTime);
}