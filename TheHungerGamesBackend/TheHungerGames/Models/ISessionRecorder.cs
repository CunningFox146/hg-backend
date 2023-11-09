using System.Text.Json.Serialization;

namespace TheHungerGames.Models;

public interface ISessionRecorder
{
    [JsonIgnore] string SessionId { get; set; }
}