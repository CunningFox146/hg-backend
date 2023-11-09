using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheHungerGames.Models;

public class GameModel : IDateRegistered, ISessionRecorder
{
    [JsonIgnore] [Key] public string SessionId { get; set; }
    public int Seed { get; set; }
    public DateTime GameStart { get; set; }
    public DateTime? LastSave { get; set; }
    public DateTime? GameEnd { get; set; }
    public DateTime Registered { get; set; }
    public GameArena Arena { get; set; }
    public GameModifier Modifier { get; set; }
}

public enum GameModifier
{
    Airdrops = 0,
    AggressiveSeasons = 1,
    NoOcean = 2,
    BuffedOcean = 3,
    None = 7,
}

public enum GameArena
{
    Gorge = 0,
    Atrium = 1,
    Moon = 2, 
    Forge = 3,
    Archives = 4,
    Water = 5
}