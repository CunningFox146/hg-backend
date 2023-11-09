using Microsoft.AspNetCore.Mvc;
using TheHungerGames.ModelProviders;

namespace TheHungerGames.Models;

public class AnalyticsEvent
{
    public AnalyticsEventType EventType { get; set; }
    public string SessionId { get; set; }
    public DateTime? Registered { get; set; }
    public int Key { get; set; }
    public string EventArgs { get; set; }
}