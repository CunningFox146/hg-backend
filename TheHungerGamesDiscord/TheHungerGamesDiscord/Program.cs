using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace TheHungerGamesDiscord;

public class Program
{
    private DiscordSocketClient _client;
    public static Task Main(string[] args) => new Program().MainAsync();

    private async Task MainAsync()
    {
        
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json");
        IConfiguration configuration = builder.Build();
        
        _client = new DiscordSocketClient();

        _client.Log += Log;
        
        var token = configuration["DiscordToken"];
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }
    
    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}