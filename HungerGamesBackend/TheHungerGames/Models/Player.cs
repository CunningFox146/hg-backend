using System.ComponentModel.DataAnnotations;

namespace TheHungerGames.Models;

public class Player
{
    [Key] public string PlayerId { get; set; }
    public string Name { get; set; }
    public string Steam { get; set; }
    public string Discord { get; set; }
    public string Description { get; set; }
}