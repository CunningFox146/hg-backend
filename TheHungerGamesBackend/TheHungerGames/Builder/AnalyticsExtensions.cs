using TheHungerGames.Systems.Analytics;
using TheHungerGames.Systems.Analytics.Handlers;

namespace TheHungerGames.Builder;

public static class AnalyticsExtensions
{
    public static IServiceCollection BindAnalytics(this IServiceCollection services)
    {
        services.AddScoped<IAnalyticsEventHandler, GameLaunchHandler>();
        services.AddScoped<IAnalyticsEventHandler, GameSaveHandler>();
        services.AddScoped<IAnalyticsEventHandler, DeathHandler>();
        services.AddScoped<IAnalyticsEventHandler, GameServerCrashHandler>();
        services.AddScoped<IAnalyticsEventHandler, PlayerCrashHandler>();
        services.AddScoped<IAnalyticsEventHandler, PlayerWinHandler>();
        services.AddScoped<IAnalyticsEventHandler, PlayerJoinHandler>();
        services.AddScoped<IAnalyticsEventHandler, PlayerDescriptionChangeHandler>();
        services.AddScoped<IAnalyticsEventHandler, PlayerDiscordChangeHandler>();
        services.AddScoped<IAnalyticsEventHandler, GameEndHandler>();
        services.AddScoped<IAnalyticsEventHandler, CharacterPickHandler>();
        services.AddScoped<IAnalyticsEventHandler, ArenaHandler>();
        
        services.AddScoped<IAnalyticsSystem, AnalyticsSystem>();

        return services;
    }
}