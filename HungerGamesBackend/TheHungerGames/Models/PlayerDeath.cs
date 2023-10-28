using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheHungerGames.Models;

public class PlayerDeath : IDateRegistered, ISessionRecorder
{
    [JsonIgnore] [Key] public Guid Id { get; set; }

    public string PlayerId { get; set; }
    public string Killer { get; set; }
    public DateTime Registered { get; set; }
    public int SessionId { get; set; }
}