using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheHungerGames.Models;

public class PlayerCrash : ISessionRecorder
{
    [JsonIgnore] [Key] public Guid Id { get; set; }
    public string PlayerId { get; set; }
    public string CrashMessage { get; set; }
    public string SessionId { get; set; }
}