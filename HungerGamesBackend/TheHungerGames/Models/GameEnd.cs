namespace TheHungerGames.Models;

public class GameEnd : IDateRegistered, ISessionRecorder
{
    public DateTime Registered { get; set; }
    public int SessionId { get; set; }
}