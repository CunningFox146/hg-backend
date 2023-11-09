namespace TheHungerGames.Models;

public class GameEnd : IDateRegistered, ISessionRecorder
{
    public DateTime Registered { get; set; }
    public string SessionId { get; set; }
}