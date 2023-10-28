using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TheHungerGames.Models;

public class PlayerItem : IDateRegistered
{
    [JsonIgnore] [Key] public Guid Id { get; set; }
    public string ItemId { get; set; }
    public string OwnerId { get; set; }
    public DateTime Registered { get; set; }
}