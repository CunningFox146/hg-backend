using System.Text.Json.Serialization;

namespace TheHungerGames.Models;

public interface ISessionRecorder
{
    [JsonIgnore] int SessionId { get; set; }
}