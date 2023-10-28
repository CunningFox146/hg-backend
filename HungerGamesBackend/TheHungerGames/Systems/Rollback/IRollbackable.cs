namespace TheHungerGames.Systems.Rollback;

public interface IRollbackable
{
    Task Rollback(int sessionId, DateTime targetTime);
}