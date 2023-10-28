using System.ComponentModel.DataAnnotations;

namespace TheHungerGames.Models;

public class RatingDelta : ISessionRecorder, IDateRegistered
{
    [Key] public Guid Id { get; set; }
    public int SessionId { get; set; }
    public DateTime Registered { get; set; }
    public string PlayerId { get; set; }
    public double Value { get; set; }
}