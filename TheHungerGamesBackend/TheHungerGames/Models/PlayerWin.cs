using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheHungerGames.Models;

public class PlayerWin : IDateRegistered, ISessionRecorder
{
    [Key] [JsonIgnore] public Guid Id { get; set; }

    public string PlayerId { get; set; }
    public DateTime Registered { get; set; }
    public string SessionId { get; set; }
}