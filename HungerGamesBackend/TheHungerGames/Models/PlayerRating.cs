using System.ComponentModel.DataAnnotations;

namespace TheHungerGames.Models;

public class PlayerRating
{
    [Key] public string PlayerId { get; set; }
    public double Rating { get; set; }
    public uint Season { get; set; }
}