using System.Text.Json.Serialization;

namespace TheHungerGames.Models;

public interface IDateRegistered
{
    DateTime Registered { get; set; }
}