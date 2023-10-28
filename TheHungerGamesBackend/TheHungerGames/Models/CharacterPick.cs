using System.ComponentModel.DataAnnotations;

namespace TheHungerGames.Models;

public class CharacterPick : IDateRegistered, ISessionRecorder
{
    [Key] public Guid Id { get; set; }
    public string Character { get; set; }
    public string Skin { get; set; }
    public string PlayerId { get; set; }

    public DateTime Registered { get; set; }
    public int SessionId { get; set; }
}