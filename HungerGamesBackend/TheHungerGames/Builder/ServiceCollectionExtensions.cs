namespace TheHungerGames.Builder;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScopedSelfAndInterfaces<TService>(this IServiceCollection services)
        where TService : class
    {
        var type = typeof(TService);
        services.AddScoped<TService>();
        foreach (var service in type.GetInterfaces())
        {
            services.AddScoped(service, s => s.GetService(type));
        }

        return services;
    }
}