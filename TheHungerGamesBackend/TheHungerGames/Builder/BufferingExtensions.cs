namespace TheHungerGames.Builder;

public static class BufferingExtensions
{
    public static IApplicationBuilder UseBuffering(this IApplicationBuilder builder)
    {
        return builder.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next();
        });
    }
}