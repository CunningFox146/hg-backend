using System.ComponentModel.DataAnnotations;

namespace TheHungerGames.Models;

public class ArenaSurvivor : IDateRegistered, ISessionRecorder
{
    [Key] public Guid Id { get; set; }
    public string PlayerId { get; set; }
    
    public DateTime Registered { get; set; }
    public int SessionId { get; set; }
}