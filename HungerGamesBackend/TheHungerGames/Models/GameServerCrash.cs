using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheHungerGames.Models;

public class GameServerCrash : IDateRegistered, ISessionRecorder
{
    [JsonIgnore] [Key] public Guid Id { get; set; }
    public string CrashMessage { get; set; }
    public DateTime Registered { get; set; }
    public int SessionId { get; set; }
}